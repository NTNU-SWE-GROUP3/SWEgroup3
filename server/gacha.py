import random
import mysql.connector
from flask import Flask, request, jsonify, Blueprint
import func

gacha = Blueprint('gacha',__name__, url_prefix='/gacha')

@gacha.route("/draw", methods=["POST"])
def Draw():
    try:
        mode = request.form.get("mode")  # coin or cash
        token_id = request.form.get("token_id")
        times = request.form.get("times")  # 1 or 10
        coinsRequired = Cost(times, mode)

        resultCards = []
        check = False
        error_message = ""
        total_coins = 0

        playerId = func.GetAccountId(token_id)
        if playerId == -2 or playerId == -1:
            error_message = "Token Expired, please login again"
            resultCards.append(
                    {
                        "id": -2,
                        "type": "error",
                        "note": error_message,
                    }
                )
            response = resultCards
            return jsonify(response)

        print("player: ", playerId)
        print("mode: ", mode)
        print("times: ", times)
        print("coins required: ", coinsRequired)

        if (mode == "coin"):
            check = CoinCheck(playerId, coinsRequired)
            error_message = "You don't have enough coins."
            total_coins = -Cost(times,mode)
            print(total_coins)
        elif mode =="cash":
            check = PaySuccess(playerId)
            error_message = "Transaction failed."

        if check:
            N = int(times)
            print(N)
            for i in range(N):
                type = DetermineType(mode)
                cardId = 0
                cardProb = 0
                coinValues = [100, 200, 300, 400, 500]
                coinWeight = [0.4,0.25,0.2,0.15,0.05]
                coinValue = 0

                if type == "coins":
                    coinValue = random.choices(coinValues,coinWeight)[0]
                    print(coinValue)
                    total_coins += coinValue
                    # UpdatePlayerCoins(playerId, -coinValue)
                else:
                    selectedCard = RandomlySelectCard(type)
                    cardId = selectedCard[0]
                    if ExistingCard(playerId,cardId,type):
                        cardProb = "-1"
                        total_coins += 500
                    else:
                        cardProb = selectedCard[1]
                        InsertCard(playerId, cardId, type)

                resultCards.append(
                    {
                        "id": cardId,
                        "type": type,
                        "note": cardProb if cardProb != 0 else coinValue,
                    }
                )

            response = resultCards
            print(total_coins)
            UpdatePlayerCoins(playerId, total_coins)
        else:
                resultCards.append(
                    {
                        "id": -2,
                        "type": "error",
                        "note": error_message,
                    }
                )
                response = resultCards
        return jsonify(response)
    
    except Exception as e:
        return jsonify({"error": str(e)}), 500
    
def Cost(times, mode):
    times = int(times)
    if mode == "coin":
        if times == 1:
            return 1000
        elif times == 10:
            return 9000
        else:
            return 0
    elif mode == "cash":
        if times == 1:
            return 30
        elif times == 10:
            return 270
        else:
            return 0
    else:
        return 0


def CoinCheck(playerId, coinsRequired):
    try:
        connection = func.create_mysql_connection()

        if not connection:
            print("Failed to connect to the database.")
            return False

        cursor = connection.cursor()
        cursor.execute("SELECT coin FROM account_data WHERE account_id = %s", (playerId,))

        playerCoins = cursor.fetchone()

        if playerCoins and playerCoins[0] >= coinsRequired:
            # if coins are enough, deduct player's coins.
            connection.close()
            return True
        else:
            connection.close()
            print("You don't have enough coins.")
            return False  # Display error message.
    except Exception as e:
        print("Error in CoinCheck:", e)
        return False


def UpdatePlayerCoins(playerId, deductCoins):
    try:
        connection = func.create_mysql_connection()
        
        cursor = connection.cursor()
        cursor.execute(
            "UPDATE account_data SET coin = coin + %s WHERE account_id = %s",
            (deductCoins, playerId),
        )
        connection.commit()
        print("Id: ", playerId, ", loss coin: ", deductCoins)
        connection.close()
    except Exception as e:
        print("Error in UpdatePlayerCoins:", e)
        return False


def DetermineType(mode):
    if mode == "coin":
        probabilities = [0.15, 0.15, 0.7]
        cardTypes = ["skill", "card_style", "coins"]
        chosen_type = random.choices(cardTypes, probabilities)[0]
    elif mode == "cash":
        probabilities = [0.225, 0.225, 0.55]
        cardTypes = ["skill", "card_style", "coins"]
        chosen_type = random.choices(cardTypes, probabilities)[0]

    print("draw result type: " + chosen_type)

    return chosen_type


def GetCardData(types):
    try:
        connection = func.create_mysql_connection()
        if not connection:
            print("Failed to connect to the database.")
            return None

        cursor = connection.cursor()

        if types == "skill":
            print("Get skill card")
            cursor.execute("SELECT skill_id, skill_probability FROM skill")
        elif types == "card_style":
            print("Get card style")
            cursor.execute("SELECT card_style_id, card_style_probability FROM card_style")
        else:
            return None

        response = cursor.fetchall()
        connection.close()

        return response
    except Exception as e:
        print("Error in GetCardData:", e)
        return None


def RandomlySelectCard(cardType):
    try:
        allCards = GetCardData(cardType)
        # print(allCards)
        allCards = sorted(allCards, key=lambda x: x[1])
        # print(allCards)

        if allCards is None:
            print("No cards found")
            raise ValueError("No cards found")

        num = random.uniform(0, 1)
        cumulativeProb = 0

        for card in allCards:
            cumulativeProb += card[1]
            if num <= cumulativeProb:
                print(card)
                return card
    except Exception as e:
        print("Error in RandomlySelectCard:", e)
        raise ValueError("Error in RandomlySelectCard")


def ExistingCard(playerId, cardId, cardType):
    try:
        connection = func.create_mysql_connection()
        cursor = connection.cursor()
        print("Checking for existing")

        if cardType == "skill":
            cursor.execute(
                "SELECT id FROM account_skill WHERE account_id = %s and skill_id = %s",
                (playerId, cardId),
            )
        elif cardType == "card_style":
            cursor.execute(
                "SELECT id FROM account_card_style WHERE account_id = %s and card_style_id = %s",
                (playerId, cardId),
            )
        else:
            return False
        existingCard = cursor.fetchone()
        connection.close()

        if existingCard:
            print("player ", playerId, "already has this card!")
            return True

        return False
    except Exception as e:
        print("Error in ExistingCard:", e)
        return False


def InsertCard(playerId, cardId, cardType):
    try:
        connection = func.create_mysql_connection()

        if not connection:
            print("Failed to connect to the database.")
            return False
        
        cursor = connection.cursor()

        if cardType == "skill":
            cursor.execute(
                "INSERT INTO account_skill (account_id, skill_id) VALUES (%s, %s)",
                (playerId, cardId),
            )
        elif cardType == "card_style":
            cursor.execute(
                "INSERT INTO account_card_style (account_id, card_style_id) VALUES (%s, %s)",
                (playerId, cardId),
            )
        else:
            return  # jsonify({"message": "Insertion error"})

        connection.commit()
        connection.close()

        print("insert ", cardType, "card_id: ", cardId)
    except Exception as e:
        print("Error in InsertCard:", e)


def PaySuccess(playerId):
    return True
