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
getplayerdata Request
input
    token
output
    status
        400004 : Success
        403011 : Token Expired
============================== '''
@user_information.route('/getplayerdata', methods=['POST'])
def GetNickname():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Input
    token = request.form.get('Token')

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
    email = conn.GetUserEmail(token)
    totalgame = conn.GetUsertotalgame(token)
    totalwin = conn.GetUsertotalwin(token)
    if(totalgame==0):
        winrate = 0
    else:
        winrate = totalwin / totalgame
    ranking = conn.GetUserrank(token)
    coin = conn.GetUsercoin(token)
    level = conn.GetUserlevel(token)
    #return nickname
    return jsonify(status = "400004",
                   msg = "Success",
                   nickname = nickname,
                   email = email,
                   totalgame = totalgame,
                   winrate = winrate,
                   totalwin = totalwin,
                   ranking = ranking,
                   coin = coin,
                   level = level
                   )
                   
