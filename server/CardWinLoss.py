from CardDatabase import cards

def WinLoss(playerCardId,opponentCardId,isRevolution):

    win = 1 # 1 means player win , -1 means opponent win , 0 means draw
    Trojan = False # True means activate Trojan skill

    if playerCardId == 9 or opponentCardId == 9:

        if playerCardId == 9 :
            win = 1
        else:
            win = -1
    
    else:
    
        if (cards[playerCardId]["cardName"] == "國王" and (cards[opponentCardId]["cardName"] == "王子" or cards[opponentCardId]["cardName"] == "騎士" or cards[opponentCardId]["cardName"] == "平民")):
        
            if (isRevolution == False):
                win = 1# 玩家贏
            
            else:
            
                win = -1# 對手贏
            
            if (isRevolution == False and opponentCardId == 17):
            
                Trojan = True
            
        
        elif (cards[opponentCardId]["cardName"] == "國王" and (cards[playerCardId]["cardName"] == "王子" or cards[playerCardId]["cardName"] == "騎士" or cards[playerCardId]["cardName"] == "平民")):
         
            
            if (isRevolution == False) :
                win = -1# 對手贏
             
            else:
             
                win = 1# 玩家贏

            if (isRevolution == False and playerCardId == 17):
             
                Trojan = True
             
         
        elif (cards[playerCardId]["cardName"] == "皇后" and (cards[opponentCardId]["cardName"] == "國王" or cards[opponentCardId]["cardName"] == "騎士" or cards[opponentCardId]["cardName"] == "平民")):
         
            if (isRevolution == False) :
                win = 1# 玩家贏
             
            else:
             
                win = -1# 對手贏
             
            if isRevolution == False and opponentCardId == 17:
             
                Trojan = True
             
         
        elif (cards[opponentCardId]["cardName"] == "皇后" and (cards[playerCardId]["cardName"] == "國王" or cards[playerCardId]["cardName"] == "騎士" or cards[playerCardId]["cardName"] == "平民")):
         
            if (isRevolution == False) :
                win = -1# 對手贏
             
            else:
             
                win = 1# 玩家贏
             
            if (isRevolution == False and playerCardId == 17):
             
                Trojan = True
             
         
        elif (cards[playerCardId]["cardName"] == "王子" and (cards[opponentCardId]["cardName"] == "皇后" or cards[opponentCardId]["cardName"] == "騎士" or cards[opponentCardId]["cardName"] == "平民")):
         
            if (isRevolution == False) :
                win = 1# 玩家贏
             
            else:
             
                win = -1# 對手贏
             
            if (isRevolution == False and opponentCardId == 17):
             
                Trojan = True
             
         
        elif (cards[opponentCardId]["cardName"] == "王子" and (cards[playerCardId]["cardName"] == "皇后" or cards[playerCardId]["cardName"] == "騎士" or cards[playerCardId]["cardName"] == "平民")):
         
            if (isRevolution == False) :
                win = -1# 對手贏
             
            else:
             
                win = 1# 玩家贏
             
            if (isRevolution == False and playerCardId == 17):
             
                Trojan = True
             
         
        elif (cards[playerCardId]["cardName"] == "騎士" and (cards[opponentCardId]["cardName"] == "殺手" or cards[opponentCardId]["cardName"] == "平民")):
         
            
            if (isRevolution == False) :
                win = 1# 玩家贏
             
            else:
             
                win = -1# 對手贏
             
            if (isRevolution == False and opponentCardId == 17):
             
                Trojan = True
             
         
        elif (cards[opponentCardId]["cardName"] == "騎士" and (cards[playerCardId]["cardName"] == "殺手" or cards[playerCardId]["cardName"] == "平民")):
         
            if (isRevolution == False) :
                win = -1
                # 對手贏
             
            else:
             
                win = 1# 玩家贏
             
            if (isRevolution == False and playerCardId == 17):
             
                Trojan = True
             
         
        elif (cards[playerCardId]["cardName"] == "殺手" and (cards[opponentCardId]["cardName"] == "國王" or cards[opponentCardId]["cardName"] == "王子" or cards[opponentCardId]["cardName"] == "皇后")):
         
            
            if (isRevolution == False):
             
                win = 1# 玩家贏
             
            else:
             
                win = -1# 對手贏
             
            
         
        elif (cards[opponentCardId]["cardName"] == "殺手" and (cards[playerCardId]["cardName"] == "國王" or cards[playerCardId]["cardName"] == "王子" or cards[playerCardId]["cardName"] == "皇后")):
         
            if (isRevolution == False):
             
                win = -1# 對手贏
             
            else:
             
                win = 1# 玩家贏
             
         
        else:
         
            # 平手
            win = 0

    return [win,Trojan]
            
             

            
         
        
     