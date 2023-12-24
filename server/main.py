from flask import Flask, request, jsonify
from gacha import gacha
from game import gaming
import mysql.connector
import func

from card import card
from skill import skill
from gameStart import gameStart
from gameTurn import gameTurn

import random

from flask_mail import Mail
from account import account
from forget_password import forget_password
from user_information import user_information
from user_data import user_data
from skill_style import skill_style
from card_style import card_style
import logging


app = Flask(__name__)

app.config.update(
    #  gmail
    MAIL_SERVER='smtp.gmail.com',
    MAIL_PORT=587,
    MAIL_USE_TLS=True,
    MAIL_USERNAME='swe3onlinegame@gmail.com',
    MAIL_PASSWORD='xybcnshquazoqoux'
)
mail = Mail(app)
LOGFILE = "app.log"
app.logger.setLevel(logging.DEBUG)
fh = logging.FileHandler(LOGFILE)
fh.setLevel(logging.DEBUG)
formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
fh.setFormatter(formatter)
app.logger.addHandler(fh)

app.register_blueprint(gacha)
app.register_blueprint(gaming)
app.register_blueprint(account)
app.register_blueprint(forget_password)
app.register_blueprint(user_information)
app.register_blueprint(user_data)
app.register_blueprint(skill_style)
app.register_blueprint(card_style)
app.register_blueprint(card)
app.register_blueprint(skill)
app.register_blueprint(gameStart)
app.register_blueprint(gameTurn)

@app.route("/")
def index():
    try:
        connection = func.create_mysql_connection()
        cursor = connection.cursor(dictionary=True)
        print("connection sucess")
        cursor.execute("SELECT * FROM account")
        data = cursor.fetchall()
        cursor.close()
        connection.close()
        return jsonify(data)
    except Exception as e:
        return jsonify({"error": str(e)}), 500


# =================
# test: http GET
# =================
@app.route("/api/unity")
def get():
    message = "Hello Unity"
    # return jsonify(text = message)
    return message


if __name__ == "__main__":
    app.run(host="0.0.0.0", port="5050", debug=False)