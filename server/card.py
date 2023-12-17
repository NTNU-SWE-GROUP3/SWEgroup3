from flask import Flask, request, jsonify,Blueprint
import json
from gameClass import Room,Player,CardSet,SkillSet
import gameClass
from gameStart import room_list
import threading
import time

card = Blueprint("card", __name__, url_prefix="/api")



def timer(time_up_event):
    time_up_event
    time.sleep(3)  # Wait for 3 seconds
    time_up_event.set()  # Signal that the time is up

async def wait_card(room):
    time_up_event = threading.Event()
    timer_thread = threading.Thread(target=timer(time_up_event))
    timer_thread.start()
    while (room.player1CurCardId == -1 or room.player2CurCardId == -1):
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
    playerRoom = gameClass.creat_room(1,-1,'none','none')
    for room in room_list:
        if room.roomId == roomId:
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


    if(index == -1):
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

    #turn end
    playerRoom.player2TurnStart = False
    playerRoom.player2TurnStart = False
    playerRoom.player1CurSkillId = -2
    playerRoom.player2CurSkillId = -2
    playerRoom.player1CurCardId = -1
    playerRoom.player2CurCardId = -1

    print(response_data)
    playerRoom.start_timer(30) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)
