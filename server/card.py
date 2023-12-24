from flask import Flask, request, jsonify,Blueprint
import json
from gameClass import Room,Player,CardSet,SkillSet
import gameClass
from gameStart import room_list
import threading
import time

card = Blueprint("card", __name__, url_prefix="/api")



def timer(time_up_event,room):
    start_time = time.time()
    timeUp = True
    while time.time() - start_time < 30:
        if room.player1CurCardId != -1 and room.player2CurCardId != -1:
            timeUp = False
            break
        time.sleep(1)
    if timeUp:
        time_up_event.set()  # Signal that the time is up

async def wait_card(room):
    time_up_event = threading.Event()
    timer_thread = threading.Thread(target=timer(time_up_event,room))
    timer_thread.start()
    
    if time_up_event.is_set():
        return -1
    return 0


@card.route('/cardSelection', methods=['POST'])
async def handle_Card():
    data = request.get_json()
    print('cardSelection:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    playerToken = data["playerToken"]
    #here should verify player token
    cardId = data["playerCardID"]
    isPlayer1 = False
    roomIndex = -1
    for room in room_list:
        if room.roomId == roomId:
            roomIndex = room_list.index(room)
            if room.player1.token == playerToken:
                room.stop_timer()
                isPlayer1 = True
                if cardId not in room.player1.card_set.card_ids:
                    room.player1CurCardId = -1
                    room.player2CurCardId = -1
                    print('cardError : card is used or not existed.')
                    errMessage = 'card is used or not existed.'
                    response_data = dict(OpponentCardId = -1,errMessage = errMessage )
                    return jsonify(response_data)
                
                index = room.player1.card_set.card_ids.index(cardId)
                room.player1CurCardId = room.player1.card_set.card_ids.pop(index)
                
                playerRoom = room
                break
            elif room.player2.token == playerToken:
                room.stop_timer()
                if cardId not in room.player2.card_set.card_ids:
                    room.player1CurCardId = -1
                    room.player2CurCardId = -1
                    print('cardError : card is used or not existed.')
                    errMessage = 'card is used or not existed.'
                    response_data = dict(OpponentCardId = -1,errMessage = errMessage )
                    return jsonify(response_data)
                
                index = room.player2.card_set.card_ids.index(cardId)
                room.player2CurCardId = room.player2.card_set.card_ids.pop(index)
                playerRoom = room
                break

    playerRoom = room_list[roomIndex]
    if(roomIndex == -1):
        response_data = dict(OpponentCardId = -1,errMessage = 'The room does not exist.' )

    retStat = await wait_card(playerRoom)
    if retStat == 0:
        if isPlayer1:
            cardId = playerRoom.player2CurCardId
            response_data = dict(OpponentCardId = cardId ,errMessage = 'Success')
        else:
            cardId = playerRoom.player1CurCardId
            response_data = dict(OpponentCardId = cardId ,errMessage = 'Success')
    else:
        errMessage = 'Only received card from you.'
        response_data = dict(OpponentCardId = -1,errMessage = errMessage )


    print(response_data)
    playerRoom.turn_end()
    playerRoom.start_timer(60) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)
