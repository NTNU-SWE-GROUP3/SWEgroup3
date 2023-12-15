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
        self.timer_thread = None
        self.time_is_up = False
        self.timerStopped = False

    def run_timer(self, time_limit):
        start_time = time.time()
        print(f'room(id:{self.roomId}) timer start')
        while time.time() - start_time < time_limit:
            if self.timerStopped:
                break
            time.sleep(1)
        print(f'runTimer:{self.timerStopped}')
        if self.timerStopped == False:
            print(f'room(id:{self.roomId}) time is up, roomId set to -1')
            self.time_is_up = True
            self.roomId = -1 # the system should remove the room whose roomId is -1.
        self.timerStopped = False

    def start_timer(self, time_limit):
        self.timerStopped = False
        timer_thread = threading.Thread(target=self.run_timer, args=(time_limit,))
        timer_thread.start()
    
    def stop_timer(self):
        print(f'room(id:{self.roomId}) timer stop')
        self.timerStopped = True
    def is_time_up(self):
        ret = self.time_is_up
        #self.time_is_up = False
        return ret


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
    print("gameClass:a=" + str(a))
    if a % 2 == 0:
        player1_cards = CardSet(set='A', card_ids=[1, 2, 3, 4, 5, 6, 7, 8, 9, 10])
        player2_cards = CardSet(set='B', card_ids=[11, 12, 13, 14, 15, 16, 17, 18, 19, 20])
    else:
        player1_cards = CardSet(set='B', card_ids=[11, 12, 13, 14, 15, 16, 17, 18, 19, 20])
        player2_cards = CardSet(set='A', card_ids=[1, 2, 3, 4, 5, 6, 7, 8, 9, 10])
    
    #here should grab data from database to check which skills are equipped.
    player1_skills = SkillSet( skill_stats=[True, True, True, True, True, True, True, True, True, True])
    player2_skills = SkillSet( skill_stats=[True, True, True, True, True, True, True, True, True, True])

    # Create player instances
    player1 = Player(token=player1_token, card_set=player1_cards, skill_set=player1_skills)
    player2 = Player(token=player2_token, card_set=player2_cards, skill_set=player2_skills)

    # Create a room and assign players
    room = Room(mode=mode,roomId=id, player1=player1, player2=player2,player1TurnStart=False,player2TurnStart=False,player1Earn=0,player2Earn=0,player1CurCardId=-1,player2CurCardId=-1,player1CurSkillId=-1,player2CurSkillId=-1)
    return room
