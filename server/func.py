import mysql.connector
from flask import Flask, request, jsonify, Blueprint
from datetime import datetime

db_config = {
    "host": "localhost",
    "user": "swegroup3",
    "password": "Swegroup3@12345",
    "database": "game",
    "auth_plugin": "mysql_native_password"
}

def create_mysql_connection():
    return mysql.connector.connect(**db_config, autocommit=True)


def GetAccountId(token_id):
    try:
        connection = create_mysql_connection()
        cursor = connection.cursor(dictionary=True)

        cursor.execute("SELECT id, token_validity FROM account WHERE token_id = %s", (token_id,))
        result = cursor.fetchone()
        print(result["id"])

        if result:
            account_id = result["id"]
            token_validity = result["token_validity"]

            if token_validity is not None and token_validity > datetime.now():
                print(f"Token validation successful for account_id {account_id}")
                return account_id
            else:
                print("Token has expired.")
                return -1
        else:
            print("Token not found.")
            return -2
    except Exception as e:
        print("Error in get_account_id:", e)
        return None
    finally:
        connection.close()
        
