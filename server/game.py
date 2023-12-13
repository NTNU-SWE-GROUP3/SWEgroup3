import mysql.connector
from flask import Flask, request, jsonify, Blueprint
import func

gaming = Blueprint("gaming", __name__, url_prefix="/gaming")

@gaming.route("/")
def index():
    return 200

@gaming.route("/get_skills_card_styles", methods=["POST"])
def GetEquipedStatus():
    try:
        token_id = request.form.get("token_id")
        account_id = func.GetAccountId(token_id)

        if account_id is not None:
            skills = GetEquippedSkills(account_id)
            card_styles = GetEquippedCardStyles(account_id)
            return jsonify({"skills": skills,"card_styles": card_styles})
        else:
            return jsonify({"skills": -1,"card_styles": -1}), 400

    except Exception as e:
        return jsonify({"skills": -1,"card_styles": -1}), 400

def GetEquippedSkills(account_id):
    try:
        connection = func.create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        # cursor.execute("SELECT skill_id FROM account_skill WHERE account_id = %s AND equip_status = 1", (account_id,))
        cursor.execute("SELECT skill_id FROM account_skill WHERE account_id = %s", (account_id,))
        results = cursor.fetchall()

        connection.close()

        skills = [result["skill_id"] for result in results]
        print(skills)
        return skills
    except Exception as e:
        print("Error in get_player_skills:", e)
        return []
    
def GetEquippedCardStyles(account_id):
    try:
        connection = func.create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        # cursor.execute("SELECT card_style_id FROM account_card_style WHERE account_id = %s AND equip_status = 1", (account_id,))
        cursor.execute("SELECT card_style_id FROM account_card_style WHERE account_id = %s", (account_id,))
        results = cursor.fetchall()

        connection.close()

        card_styles = [result["card_style_id"] for result in results]
        print(card_styles)
        return card_styles
    except Exception as e:
        print("Error in getEquippedCardStyle:", e)
        return []
    
@gaming.route("/get_player_info",methods=["POST"])
def getPlayerInfo():
    try:
        token_id = request.form.get("token_id")
        account_id = func.GetAccountId(token_id)

        connection = func.create_mysql_connection()
        cursor = connection.cursor(dictionary=True)
        cursor.execute("SELECT * FROM account_data WHERE account_id = %s", (account_id,))
        results = cursor.fetchone()

        if results is None:
            print("No results")
        print(results) 
        return jsonify(results)

    except Exception as e:
        print("Error in getPlayerInfo:", e)
        return [-1]
    finally:
        connection.close()


@gaming.route("/game_finish", methods=["POST"])
def game_finish():
    try:
        print("Game finished")
        account_id = request.form.get("account_id")
        end_status = request.form.get("end_status")

        connection = func.create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        addcoins = 0
        addexp = 0
        if end_status == "win":
            addcoins = 200
            addexp = 200
        elif end_status == "lose":
            addcoins = 50
            addexp = 50
        else:
            addcoins = 100
            addcoins = 100

        cursor.execute("UPDATE account_data SET coin = coin + %s WHERE account_id = %s", (addcoins, account_id),)
        cursor.execute("UPDATE account_data SET experience = experience + %s WHERE account_id = %s", (addexp, account_id),)

        connection.commit()

        return jsonify({"success": True, "message": "Game finished successfully"})
    except Exception as e:
        print("Error in game_finish:", e)
        return jsonify({"success": False, "message": "Internal Server Error"}), 500
    finally:
        connection.close()

