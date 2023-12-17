import mysql.connector
from flask import Flask, request, jsonify, Blueprint

db_config = {
    "host": "localhost",
    "user": "swegroup3",
    "password": "Swegroup3@12345",
    "database": "game",
}

def create_mysql_connection():
    return mysql.connector.connect(**db_config, autocommit=True)


def GetAccountId(token_id):
    try:
        connection = create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        cursor.execute("SELECT id FROM account WHERE token_id = %s", (token_id,))
        result = cursor.fetchone()
        print(result["id"])

        connection.close()

        if result:
            return result["id"]
        else:
            return None
    except Exception as e:
        print("Error in get_account_id:", e)
        return None
