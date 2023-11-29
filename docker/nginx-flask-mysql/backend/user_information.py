from flask import current_app
from flask import Blueprint
from flask import jsonify
from flask import request
from db import DBManager
#from mail import send_verification_email
import secrets, string, datetime

user_information = Blueprint('user_information', __name__, url_prefix='/user_information')

conn = None


''' ==============================
getnickname Request
input
    token
output
    status
        400004 : Success
        403011 : Token Expired
        403006 : Email & account NOT match
============================== '''
@forget_password.route('/getnickname', methods=['POST'])
def CheckAccount():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Input
    token = request.form.get('token')

    current_app.logger.info("token: %s", token)

    # # Input validation
    # # there should check if the input are dangerous

    #token expiredtime
    expiredtime = conn.GetTokenExpiredTime(token)
    current_app.logger.info("Expired time: %s", expiredtime)
    now = datetime.datetime.now()
    current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
    if (expiredtime == -1):
        return jsonify(status = "403011", msg = "No such token")
    elif(now > expiredtime):
        return jsonify(status = "403011", msg = "Token expired")

    # Success
    nickname = conn.GetUserNickname(token)

    #return nickname
    return jsonify(status = "400004", msg = nickname)
