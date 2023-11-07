import os
from flask import Flask
from flask import jsonify
from flask import request
from db import DBManager
import secrets, string, datetime


server = Flask(__name__)
conn = None

# from . import auth
# server.register_blueprint(auth.bp)

# @server.route('/')
# def listBlog():
#     global conn
#     if not conn:
#         conn = DBManager(password_file='/run/secrets/db-password')
#         conn.populate_db()
#     rec = conn.query_titles()

#     response = ''
#     for c in rec:
#         response = response  + '<div>   Hello  ' + c + '</div>'
#     return response



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



# @server.route('/api/data', methods=['POST'])
# def get_current_user():
#     user_name = 'watashi'
#     email = 'watashi@gmail.com'
#     id = 'watashi123'
#     unity_json = request.json['text']
#     return jsonify(
#         id=id,
#         userName = user_name,
#         email = email,
#         unityText = unity_json
#         )

if __name__ == '__main__':
    # server.run()
    server.run(debug=True,host='0.0.0.0')
