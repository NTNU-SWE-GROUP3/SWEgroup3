from flask import Flask, request, jsonify,Blueprint
import json
from gameClass import Room,Player,CardSet,SkillSet
from gameStart import room_list
import gameClass
import asyncio
import threading
import time

skill = Blueprint("skill", __name__, url_prefix="/api")

def useSkill(room,playerToken,SkillId,cardId):
    if SkillId == 2 and cardId != -2:#階級流動
        if room.player1.token == playerToken:
            if cardId == -1:
                room.player1CurSkillCardId = cardId
                return
            index = room.player1.card_set.card_ids.index(cardId)
            room.player1.card_set.card_ids.pop(index)
            room.player1CurSkillCardId = cardId
            if room.player1.card_set.set == 'A':
                room.player1.card_set.card_ids.append(3)
            else:
                room.player1.card_set.card_ids.append(13)
        else:
            if cardId == -1:
                room.player2CurSkillCardId = cardId
                return
            index = room.player2.card_set.card_ids.index(cardId)
            room.player2.card_set.card_ids.pop(index)
            room.player2CurSkillCardId = cardId
            if room.player2.card_set.set == 'A':
                room.player2.card_set.card_ids.append(3)
            else:
                room.player2.card_set.card_ids.append(13)
    elif SkillId == 3 and cardId != -2:#暗影轉職
        if room.player1.token == playerToken:
            if cardId == -1:
                room.player1CurSkillCardId = cardId
                return
            index = room.player1.card_set.card_ids.index(cardId)
            room.player1.card_set.card_ids.pop(index)
            room.player1CurSkillCardId = cardId
            if room.player1.card_set.set == 'A':
                room.player1.card_set.card_ids.append(5)
            else:
                room.player1.card_set.card_ids.append(14)
        else:
            if cardId == -1:
                room.player2CurSkillCardId = cardId
                return
            index = room.player2.card_set.card_ids.index(cardId)
            room.player2.card_set.card_ids.pop(index)
            room.player2CurSkillCardId = cardId
            if room.player2.card_set.set == 'A':
                room.player2.card_set.card_ids.append(5)
            else:
                room.player2.card_set.card_ids.append(14)
    elif SkillId == 9:#強制徵收
        if room.player1.token == playerToken:
            room.player2Earn -= 1
        else:
            room.player1Earn -= 1
    elif SkillId == 10:#勝者之堆
        if room.player1.token == playerToken:
            room.player1Earn += 1
        else:
            room.player2Earn += 1
    elif SkillId == 11 and cardId != -2:#簡易剔除 
        if room.player1.token == playerToken:
            if cardId == -1:
                room.player1CurSkillCardId = cardId
                return
            print(room.player2.card_set.card_ids.index)
            index = room.player2.card_set.card_ids.index(cardId)
            room.player2.card_set.card_ids.pop(index)
            room.player1CurSkillCardId = cardId

        else:
            if cardId == -1:
                room.player2CurSkillCardId = cardId
                return
            print(room.player1.card_set.card_ids.index)
            index = room.player1.card_set.card_ids.index(cardId)
            room.player1.card_set.card_ids.pop(index)
            room.player2CurSkillCardId = cardId
            



def timer(time_up_event,room):
    start_time = time.time()
    timeUp = True
    while time.time() - start_time < 10:
        if room.player1CurSkillId != -2 and room.player2CurSkillId != -2:
            timeUp = False
            break
        time.sleep(1)
    if timeUp:
        time_up_event.set()  # Signal that the time is up

async def wait_skill(room):
    time_up_event = threading.Event()
    timer_thread = threading.Thread(target=timer(time_up_event,room))
    timer_thread.start()
    while (room.player1CurSkillId == -2 or room.player2CurSkillId == -2):
        print(str(room.player1CurSkillId) + ":" + str(room.player2CurSkillId))
        if time_up_event.is_set():
            print('wait_skill : -1')
            return -1
    return 0

def timer2(time_up_event,room,player):
    start_time = time.time()
    timeUp = True
    while time.time() - start_time < 10:
        if room.player1CurSkillCardId != -2 and player == 2:
            timeUp = False
            break
        elif room.player2CurSkillCardId != -2 and player == 1:
            timeUp = False
            break
        time.sleep(1)
    if timeUp:
        time_up_event.set()  # Signal that the time is up

def timer3(time_up_event,room):
    start_time = time.time()
    timeUp = True
    while time.time() - start_time < 10:
        if room.delimmaCardId1 != -2 and room.delimmaCardId2 != -2:
            timeUp = False
            break
        time.sleep(1)
    if timeUp:
        time_up_event.set()  # Signal that the time is up

async def wait_dilemmaCards(room):
    time_up_event = threading.Event()
    timer_thread = threading.Thread(target=timer3(time_up_event,room))
    timer_thread.start()
    
    if time_up_event.is_set():
        print('wait_dilemmaCards : -2')
        return -2
    return 0

async def wait_skillCheck(room,player):
    time_up_event = threading.Event()
    timer_thread = threading.Thread(target=timer2(time_up_event,room,player))
    timer_thread.start()
    
    if time_up_event.is_set():
        print('wait_skill_check : -2')
        return -2
    
    if(player == 2):
        return room.player1CurSkillCardId
    else:
        return room.player2CurSkillCardId

@skill.route('/dilemmaUse', methods=['POST'])
async def handle_dilemmaUse():
    data = request.get_json()
    print('skill:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    playerToken = data["playerToken"]
    cardId1 = data["cardId1"]
    cardId2 = data["cardId2"]
    #here should verify player token

    index = -1
    for room in room_list:
        if room.roomId == roomId:
            if room.player1.token == playerToken:
                room.stop_timer()
                index = room_list.index(room)
                room.delimmaCardId1 = cardId1
                room.delimmaCardId2 = cardId2
            elif room.player2.token == playerToken:
                room.stop_timer()
                index = room_list.index(room)
                room.delimmaCardId1 = cardId1
                room.delimmaCardId2 = cardId2
            break
    
    playerRoom = room_list[index]
    if(index == -1):
        response_data = dict(state = -1,errMessage = 'The room does not exist.' )
    else:
        response_data = dict(state = 1,errMessage = 'Received dilemmaCards.' )
        
    print(response_data)
    playerRoom.start_timer(30) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)

@skill.route('/dilemmaUseCheck', methods=['POST'])
async def handle_dilemmaUseCheck():
    data = request.get_json()
    print('skill:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    playerToken = data["playerToken"]

    #here should verify player token

    index = -1
    for room in room_list:
        if room.roomId == roomId:
            if room.player1.token == playerToken:
                room.stop_timer()
                index = room_list.index(room)
            elif room.player2.token == playerToken:
                room.stop_timer()
                index = room_list.index(room)
            break
    
    playerRoom = room_list[index]
    if(index == -1):
        response_data = dict(cardId1 = -1,cardId2 = -1,errMessage = 'The room does not exist.' )
    ret = await wait_dilemmaCards(playerRoom)

    if ret == -2:
        response_data = dict(cardId1 = -1,cardId2 = -1,errMessage = 'Did not receive cards.' )
    else:
        response_data = dict(cardId1 = playerRoom.delimmaCardId1,cardId2 = playerRoom.delimmaCardId2,errMessage = 'Received dilemmaCards from opponent.' )
        
    print(response_data)
    playerRoom.start_timer(30) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)

@skill.route('/useSkill', methods=['POST'])
async def handle_useSkill():
    data = request.get_json()
    print('useSkill:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    playerToken = data["playerToken"]
    #here should verify player token
    skillId = data["playerSkillID"]
    cardId = data["cardId"]
    
    isPlayer1 = False
    index = -1
    for room in room_list:
        if room.roomId == roomId:
            room.stop_timer()
            index = room_list.index(room)
            useSkill(room,playerToken,skillId,cardId)#for skill 2 and 3
            break
    
    playerRoom = room_list[index]
    if(index == -1):
        response_data = dict(OpponentSkillId = -1,errMessage = 'The room does not exist.' )
    else:    
        response_data = dict(OpponentSkillId = 1,errMessage = 'Skill use Success' )

    print(response_data)
    playerRoom.start_timer(30) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)

@skill.route('/useSkillCheck', methods=['POST'])#for skill 2 and 3
async def handle_useSkillCheck():
    data = request.get_json()
    print('useSkillCheck:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    playerToken = data["playerToken"]

    
    isPlayer1 = False
    index = -1
    ret = -2
    for room in room_list:
        if room.roomId == roomId:
            room.stop_timer()
            index = room_list.index(room)
            if room.player1.token == playerToken:
                ret = await wait_skillCheck(room,1)
            elif room.player2.token == playerToken:
                ret = await wait_skillCheck(room,2)
            break
    
    playerRoom = room_list[index]
    if(index == -1):
        response_data = dict(cardId = -1,errMessage = 'The room does not exist.' )
    else:    
        if ret != -2:
            response_data = dict(cardId = ret,errMessage = 'SkillCard receive Success' )
        else:
            response_data = dict(cardId = ret,errMessage = 'Did not receive card from opponent' )
    print(playerToken)
    print(response_data)
    playerRoom.start_timer(30) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)

@skill.route('/skill', methods=['POST'])
async def handle_skill():
    data = request.get_json()
    print('skill:')
    print(data)   
    gameType = data["gameType"]
    roomId = data["roomId"]
    playerToken = data["playerToken"]
    #here should verify player token
    skillId = data["playerSkillID"]
    
    isPlayer1 = False
    index = -1
    for room in room_list:
        if room.roomId == roomId:
            if room.player1.token == playerToken:
                room.stop_timer()
                isPlayer1 = True
                if skillId == -1:#player didn't use skill
                    room.player1CurSkillId = skillId
                    index = room_list.index(room)
                    break
                if room.player1.skill_set.skill_stats[skillId] == True:
                    room.player1.skill_set.skill_stats[skillId] = False
                    room.player1CurSkillId = skillId
                    useSkill(room,playerToken,skillId,-1) #for skill 9 and 10
                    index = room_list.index(room)
                    break
                else:
                    room.player1CurSkillId = -2
                    room.player2CurSkillId = -2
                    print('skillError : skill is used or not equipped.')
                    errMessage = 'skill is used or not equipped.'
                    response_data = dict(OpponentSkillId = -1,errMessage = errMessage )
                    return jsonify(response_data)

            elif room.player2.token == playerToken:
                room.stop_timer()
                if skillId == -1:#player didn't use skill
                    room.player2CurSkillId = skillId
                    index = room_list.index(room)
                    break
                if room.player2.skill_set.skill_stats[skillId] == True:
                    room.player2.skill_set.skill_stats[skillId] = False
                    room.player2CurSkillId = skillId
                    useSkill(room,playerToken,skillId,-1) #for skill 9 and 10
                    index = room_list.index(room)
                    break
                else:
                    room.player1CurSkillId = -2
                    room.player2CurSkillId = -2
                    print('skillError : skill is used or not equipped.')
                    errMessage = 'skill is used or not equipped.'
                    response_data = dict(OpponentSkillId = -1,errMessage = errMessage )
                    return jsonify(response_data)
            else:
                response_data = dict(OpponentSkillId = -1,errMessage = 'You are not in this room' )
                return jsonify(response_data)
    
    playerRoom = room_list[index]
    if(index == -1):
        response_data = dict(OpponentSkillId = -1,errMessage = 'The room does not exist.' )

    print('start to wait  ' + str(playerRoom.player1CurSkillId) + ":" + str(playerRoom.player2CurSkillId))
    
    retStat = await wait_skill(playerRoom)

    if retStat == 0:
        if isPlayer1:
            skillId = playerRoom.player2CurSkillId
            response_data = dict(OpponentSkillId = skillId ,errMessage = 'Success')
        else:
            skillId = playerRoom.player1CurSkillId
            response_data = dict(OpponentSkillId = skillId ,errMessage = 'Success')
    else:
        errMessage = 'Only received skill from you.'
        response_data = dict(OpponentSkillId = -1,errMessage = errMessage )

    print(response_data)
    playerRoom.start_timer(30) # the room will be remvoed when no player send signals in 20 seconds.
    return jsonify(response_data)