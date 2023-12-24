import random
import threading
import time

class Room:
    def __init__(self,mode, roomId, player1, player2,player1TurnStart,player2TurnStart,player1Earn,player2Earn,player1CurCardId,player2CurCardId,player1CurSkillId,player2CurSkillId):
        self.mode = mode # 0 for PVE, 1 for PVP
        self.roomId = roomId
        self.player1 = player1
        self.player2 = player2
        self.player1TurnStart = player1TurnStart
        self.player2TurnStart = player2TurnStart
        self.player1Earn = player1Earn
        self.player2Earn = player2Earn
        self.player1CurCardId = player1CurCardId #current card id 
        self.player2CurCardId = player2CurCardId
        self.player1CurSkillId = player1CurSkillId #current skill id 
        self.player2CurSkillId = player2CurSkillId
        self.player1CurSkillCardId = -2
        self.player2CurSkillCardId = -2
        self.delimmaCardId1 = -2
        self.delimmaCardId2 = -2
        self.timer_thread = None
        self.timerStopped = False
        self.timerStarted = False
        self.turnEnd = 0

    def run_timer(self, time_limit):
        start_time = time.time()

        print(f'{start_time}room(id:{self.roomId}) timer start')
        while time.time() - start_time < time_limit:
            if self.timerStopped:
                break
            time.sleep(1)
        print(f'{time.time()} <- {start_time}runTimer:{self.timerStopped}')
        if self.timerStopped == False:
            print(f'room(id:{self.roomId}) time is up, roomId set to -1')
            self.roomId = -1 # the system should remove the room whose roomId is -1.
        self.timerStopped = False

    def start_timer(self, time_limit):
        if self.timerStarted == False:
            self.timerStarted = True
            #self.timerStopped = False
            timer_thread = threading.Thread(target=self.run_timer, args=(time_limit,))
            timer_thread.start()
        else:
            print(f'room(id:{self.roomId}) timer has started')
        
    
    def stop_timer(self):
        if self.timerStarted == True:
            self.timerStarted = False
            self.timerStopped = True
            print(f'room(id:{self.roomId}) timer stop')
        else:
            print(f'room(id:{self.roomId}) timer has stopped')
        

    def turn_end(self):
        self.turnEnd += 1
        if self.turnEnd == 2:
            self.player1TurnStart = False
            self.player2TurnStart = False
            self.player1CurSkillId = -2
            self.player2CurSkillId = -2
            self.player1CurSkillCardId = -2
            self.player2CurSkillCardId = -2
            self.delimmaCardId1 = -2
            self.delimmaCardId2 = -2
            self.player1CurCardId = -1
            self.player2CurCardId = -1
            self.turnEnd = 0


class Player:
    def __init__(self, token, card_set, skill_set):
        self.token = token
        self.card_set = card_set
        self.skill_set = skill_set

class CardSet:
    def __init__(self,set, card_ids):
        self.set = set
        self.card_ids = card_ids

class SkillSet:
    def __init__(self, skill_stats):
        self.skill_stats = skill_stats

# Example instantiation

def creat_room(mode,id,player1_token,player2_token):
    # Create card and skill sets for players
    random.seed()
    a = random.randint(0,1)
    #print("gameClass:a=" + str(a))
    if a % 2 == 0:
        player1_cards = CardSet(set='A', card_ids=[0,1, 2, 3, 4, 5, 6, 7, 8, 9])
        player2_cards = CardSet(set='B', card_ids=[10,11, 12, 13, 14, 15, 16, 17, 18, 19])
    else:
        player1_cards = CardSet(set='B', card_ids=[10,11, 12, 13, 14, 15, 16, 17, 18, 19])
        player2_cards = CardSet(set='A', card_ids=[0,1, 2, 3, 4, 5, 6, 7, 8, 9])
    
    #here should grab data from database to check which skills are equipped.
    player1_skills = SkillSet( skill_stats=[True, True, True, True, True, True, True, True, True, True])
    player2_skills = SkillSet( skill_stats=[True, True, True, True, True, True, True, True, True, True])

    # Create player instances
    player1 = Player(token=player1_token, card_set=player1_cards, skill_set=player1_skills)
    player2 = Player(token=player2_token, card_set=player2_cards, skill_set=player2_skills)

    # Create a room and assign players
    room = Room(mode=mode,roomId=id, player1=player1, player2=player2,player1TurnStart=False,player2TurnStart=False,player1Earn=0,player2Earn=0,player1CurCardId=-1,player2CurCardId=-1,player1CurSkillId=-2,player2CurSkillId=-2)
    return room
