from flask import Flask, request, jsonify,Blueprint
import asyncio
import gameStart
import gameClass
from gameStart import room_list
from gameClass import Room,Player,CardSet,SkillSet
import threading
import time

gameTurn = Blueprint("gameTurn", __name__, url_prefix="/api")


def timer(time_up_event):
    time.sleep(5)  # Wait for 10 seconds
    time_up_event.set()  # Signal that the time is up

async def wait_start(room):
    time_up_event = threading.Event()
    timer_thread = threading.Thread(target=timer(time_up_event))
    timer_thread.start()
    while (room.player1TurnStart == False or room.player2TurnStart == False):
        if time_up_event.is_set():
            return -1
    return 0

@gameTurn.route('/turnStart', methods=['POST'])
async def handle_turnStart():
    data = request.get_json()
    print('turnStart:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    playerToken = data["playerToken"]
    playerEarn = data["playerEarn"]
    opponentEarn = data["opponentEarn"]
    #here should verify player token

    index = -1
    for room in room_list:
        if room.roomId == roomId:
            if room.player1.token == playerToken:
                room.stop_timer()
                room.player1TurnStart = True
                room.player1Earn = playerEarn
                index = room_list.index(room)
                break
            elif room.player2.token == playerToken:
                room.stop_timer()
                room.player2TurnStart = True
                room.player2Earn = playerEarn
                index = room_list.index(room)
                break
            else:
                response_data = dict(state = -1,errMessage = 'You are not in this room' )
                return jsonify(response_data)

                
    playerRoom = room_list[index]
    if(index == -1):
        response_data = dict(state = -1,errMessage = 'The room does not exist.' )

    retStat = await wait_start(playerRoom)

    if retStat == 0:
        errMessage = 'start signals received by both players.'
        response_data = dict(state = 1,errMessage = errMessage )
    else:
        errMessage = 'Only received start signals from you.'
        response_data = dict(state = -1,errMessage = errMessage )
    
    
    print(response_data)
    playerRoom.start_timer(20) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)

