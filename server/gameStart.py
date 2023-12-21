from flask import Flask, request, jsonify,Blueprint
import time
from gameClass import Room,Player,CardSet,SkillSet
import gameClass
gameStart = Blueprint("gameStart", __name__, url_prefix="/api")

room_list = []

#the codes here is just for testing, it should be removed when merged to server
room = gameClass.creat_room(1,1,'ABC','XYZ')#roomId, player1_token, player2_token 
room_list.append(room)
#-------------------------------------------------------------


@gameStart.route('/getCardSet', methods=['POST'])
def handle_getCardSet():   
    data = request.get_json()
    print('getCardSet:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    playerToken = data["playerToken"]
    print("Player:" + str(roomId))
    response_data = dict(roomId=-1, playerCardSet = 'None' , opponentCardSet = 'None')
    print(room_list)
    playerRoom = gameClass.creat_room(1,-1,'none','none')
    for room in room_list:
        print(room.roomId)
        print("Player:" + str(roomId))
        if room.roomId == roomId :
            playerRoom = room
            if room.player1.token == playerToken:
                response_data = dict(roomId=room.roomId, playerCardSet = room.player1.card_set.set , opponentCardSet = room.player2.card_set.set)
            elif room.player2.token == playerToken:
                response_data = dict(roomId=room.roomId, playerCardSet = room.player2.card_set.set , opponentCardSet = room.player1.card_set.set)

    if(playerRoom.roomId == -1):
        response_data = dict(roomId=-1, playerCardSet = 'None' , opponentCardSet = 'None')
    print(response_data)

    playerRoom.time_is_up = False
    playerRoom.start_timer(30) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)


@gameStart.route('/gameStart', methods=['POST'])
def handle_request():   
    data = request.get_json()
    print('gameStart:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    player1Token = data["player1Token"]
    player2Token = data["player2Token"]
    print("Player:" + str(roomId))
    print(room_list)

    playerRoom = gameClass.creat_room( gameType, roomId, player1Token, player2Token )
    room_list.append(playerRoom)
    

    if(playerRoom.roomId == -1):
        response_data = dict(roomId=-1, playerCardSet = 'None' , opponentCardSet = 'None')
    print(response_data)

    playerRoom.time_is_up = False
    playerRoom.start_timer(30) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)

# Promising that gameType is correct: 0 for PVE, 1 for PVP
# Promising that if PVE, then Player2Token == 'computer'

