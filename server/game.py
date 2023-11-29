import mysql.connector
from flask import Flask, request, jsonify, Blueprint

gaming = Blueprint("gaming", __name__, url_prefix="/gaming")


db_config = {
    "host": "localhost",
    "user": "swegroup3",
    "password": "Swegroup3@12345",
    "database": "game",
}


def create_mysql_connection():
    return mysql.connector.connect(**db_config, autocommit=True)

@gaming.route("/")
def index():
    return 200

@gaming.route("/get_skills", methods=["POST"])
def GetSkills():
    try:
        token_id = request.form.get("token_id")
        account_id = GetAccountId(token_id)

        if account_id is not None:
            skills = GetEquippedSkills(account_id)
            return jsonify({"skills": skills})
        else:
            return jsonify({"skills": "Invalid token_id"}), 400


    except Exception as e:
        return jsonify({"skills": "Invalid token id"}), 400


@gaming.route("/get_card_styles", methods=["POST"])
def GetCardStyles():
    try:
        token_id = request.form.get("token_id")
        account_id = GetAccountId(token_id)

        if account_id is not None:
            card_styles = GetEquippedCardStyles(account_id)
            return jsonify({"card_styles": card_styles})
        else:
            return jsonify({"card_styles": "Invalid token_id"}), 400

    except Exception as e:
        return jsonify({"card_styles": "Invalid token id"}), 400

def GetAccountId(token_id):
    try:
        connection = create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        cursor.execute("SELECT id FROM account WHERE token_id = %s", (token_id,))
        result = cursor.fetchone()

        connection.close()

        if result:
            return result["id"]
        else:
            return None
    except Exception as e:
        print("Error in get_account_id:", e)
        return None

def GetEquippedSkills(account_id):
    try:
        connection = create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        cursor.execute("SELECT skill_id FROM account_skill WHERE account_id = %s AND equip_status = 1", (account_id,))
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
        connection = create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        cursor.execute("SELECT card_style_id FROM account_card_style WHERE account_id = %s AND equip_status = 1", (account_id,))
        results = cursor.fetchall()

        connection.close()

        card_styles = [result["card_style_id"] for result in results]
        print(card_styles)
        return card_styles
    except Exception as e:
        print("Error in getEquippedCardStyle:", e)
        return []