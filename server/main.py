from flask import Flask, request, jsonify
import mysql.connector
from gacha import gacha
from game import gaming
from playerInfo import info

import random

app = Flask(__name__)

# MySQL configuration
db_config = {
    "host": "localhost",
    "user": "swegroup3",
    "password": "Swegroup3@12345",
    "database": "game",
}

app.register_blueprint(gacha)
app.register_blueprint(gaming)
app.register_blueprint(info)


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

if __name__ == "__main__":
    app.run(host="0.0.0.0", port="5050", debug=False)