from flask import Flask, request, jsonify
import mysql.connector
from gacha import gacha
from game import gaming

import random

from flask_mail import Mail
from account import account
from forget_password import forget_password
from user_information import user_information
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


# MySQL configuration
db_config = {
    "host": "localhost",
    "user": "swegroup3",
    "password": "Swegroup3@12345",
    "database": "game",
}

app.register_blueprint(gacha)
app.register_blueprint(gaming)
app.register_blueprint(account)
app.register_blueprint(forget_password)
app.register_blueprint(user_information)

# Function to create a MySQL connection
def create_mysql_connection():
    return mysql.connector.connect(**db_config,autocommit=True)

@app.route("/")
def index():
    try:
        connection = create_mysql_connection()
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