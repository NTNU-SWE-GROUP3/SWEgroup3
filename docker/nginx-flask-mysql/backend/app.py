import os
from flask import Flask
from flask import jsonify
from flask import request
from db import DBManager
import secrets, string, datetime


server = Flask(__name__)
conn = None



# =================
# test: http GET
# =================
@server.route("/api/unity")
def get():
    message = "Hello Unity"
    # return jsonify(text = message)
    return message



# ===================================================
# account login
# input: account name, account password
# ===================================================
@server.route("/api/login", methods=['POST'])
def ApiLoginPost():
    accountName = request.form['account'].replace("\u200b", "")
    accountPassword = request.form['password'].replace("\u200b", "")
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')
    # server.logger.info(accountName)
    # server.logger.info(accountPassword)
    isAccount = conn.AccountExist(accountName, accountPassword)
    if(isAccount[0] == 1):
        # account exists
        alphabet = string.ascii_letters + string.digits
        tokenId = ''.join(secrets.choice(alphabet) for i in range(128))
        now = datetime.datetime.now()
        now += datetime.timedelta(days=3)
        tokenValidity = now.strftime('%Y-%m-%d %H:%M:%S')
        conn.AccountLogin(accountName, accountPassword, tokenId, tokenValidity)
        return jsonify(status = "success", tokenId = tokenId)
    else:
        # account not exists
        return jsonify(status = "failure", tokenId = "")



if __name__ == '__main__':
    # server.run()
    server.run(debug=True,host='0.0.0.0')
