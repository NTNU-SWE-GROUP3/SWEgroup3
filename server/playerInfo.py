import mysql.connector
from flask import Flask, request, jsonify, Blueprint
import func

info = Blueprint("info", __name__, url_prefix="/playerInfo")


@info.route("/")
def index():
    print("This is player info page")
    return 200


@info.route("/account_data", methods=["POST"])
def GetInfo():
    try:
        token_id = request.form.get("token_id")
        account_id = func.GetAccountId(token_id)

        if account_id is not None:
            data = GetPlayerInfo(account_id)
            return jsonify(data), 200
        else:
            return -1
    except Exception as e:
        return 400


def GetPlayerInfo(account_id):
    try:
        connection = func.create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        cursor.execute(
            "SELECT * FROM account_data WHERE account_id = %s", (account_id,)
        )
        result = cursor.fetchone()
        print(result)

        user_profile = {
            "account_id": result["account_id"],
            "nickname": result["nickname"],
            "level": result["level"],
            "experience": result["experience"],
            "rank": result["rank"],
            "total_match": result["total_match"],
            "total_win": result["total_win"],
            "ranked_winning_streak": result["ranked_winning_streak"],
            "ranked_XP": result["ranked_XP"],
            "coin": result["coin"],
        }
        print(user_profile)
        connection.close()

        return user_profile
    except Exception as e:
        print("Error in GetPlayerInfo: ", e)
        return []


# account_id
# nickname
#  level
#   experience
# | rank   |
#  total_match |
#  total_win |
#  ranked_winning_streak |
#  ranked_XP |
#  coin
