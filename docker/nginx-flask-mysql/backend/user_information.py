from flask import current_app
from flask import Blueprint
from flask import jsonify
from flask import request
from db import DBManager
from mail import send_check_newemail
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


''' ==============================
ChangeNickname Request
input
    Token
    NewNickName
output
    status
        400000 : Success
        403001 : User Name Existed
        403002 : User Name Too Long
        403003 : User Name Too Short
        403004 : User Name is illigal
        403011 : Token Expired
============================== '''
@user_information.route('/changenickname', methods=['POST'])
def ChangeNickname():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Input
    token = request.form.get('Token')
    current_app.logger.info("token: %s", token)
    nickname = request.form.get('NewNickname')
    current_app.logger.info("New Nick name: %s", token)


    #token expiredtime
    expiredtime = conn.GetTokenExpiredTime(token)
    current_app.logger.info("Expired time: %s", expiredtime)
    now = datetime.datetime.now()
    current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
    if (expiredtime == -1):
        return jsonify(status = "403011", msg = "No such token")
    elif(now > expiredtime):
        return jsonify(status = "403011", msg = "Token expired")

    if (conn.NicknameExist(nickname)):
        return jsonify(status = "403001", msg = "Nick name Existed")

    if (len(nickname)>15):
        current_app.logger.info("nickname too long")
        return jsonify(status = "403002", msg = "User Name Too Long")

    if (len(nickname)<3):
        current_app.logger.info("nickname too short")
        return jsonify(status = "403003", msg = "User Name Too Short")

    if not(nickname.isalnum()):
        return jsonify(status = "403004", msg = "User Name is illigal")

    # Change NicknameExist
    conn.UpdateNewNickname(token, nickname)
    #END
    return jsonify(status = "400000", msg = f"Success to change nickname:{nickname}")


''' ==============================
ChangeEmail Request
input
    Token
    Email
output
    status
        400000 : Success
        403005 : Email has been uesd
        403011 : Token Expired
============================== '''
@user_information.route('/changeemail', methods=['POST'])
def ChangeEmail():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Input
    token = request.form.get('Token')
    current_app.logger.info("token: %s", token)
    NewEmail = request.form.get('Email')
    current_app.logger.info("New Email: %s", NewEmail)

    #token expiredtime
    expiredtime = conn.GetTokenExpiredTime(token)
    current_app.logger.info("Expired time: %s", expiredtime)
    now = datetime.datetime.now()
    current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
    if (expiredtime == -1):
        return jsonify(status = "403011", msg = "No such token")
    elif(now > expiredtime):
        return jsonify(status = "403011", msg = "Token expired")

    userid = conn.FindAccountIDByToken(token)

    if(conn.EmailExist(NewEmail)):
        return jsonify(status = "403005", msg = "Email has been uesd")

    #Sent the mail
    random_code = send_check_newemail(userid, NewEmail)
    current_app.logger.info("Random code generated: %s", random_code)

    # Generate verifycode validity datetime
    now = datetime.datetime.now()
    now += datetime.timedelta(minutes=5)
    verifycodeValidity = now.strftime('%Y-%m-%d %H:%M:%S')
    conn.SetVerifyCodeAndValidity(userid, random_code, verifycodeValidity)

    #Success
    return jsonify(status = "400000", msg = "Email has been sent")



''' ==============================
Check New Email Request
input
    code
output
    status
        400000 : Success
        403006 :
        403007 :
============================== '''
@user_information.route('/verify_email', methods=['POST'])
def verify_email():

# DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    random_code = request.form.get('code')
    current_app.logger.info("random_code: %s", random_code)
    userid = request.form.get('user_id')
    current_app.logger.info("userid: %s", userid)
    email = request.form.get('email')
    current_app.logger.info("email: %s", email)

    expiredtime = conn.GetVerifyCodeExpiredTime(userid)
    current_app.logger.info("Expired time: %s", expiredtime)

    #expiredtime = datetime.strptime(expiredtime, '%Y-%m-%d %H:%M:%S')
    now = datetime.datetime.now()
    current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
    if (expiredtime == -1 or now > expiredtime):
        return jsonify(status = "403007", msg = "Varify mail Expired")

    if not (conn.CheckVerifyCode(userid,random_code)):
        current_app.logger.info("Wrong VerifyCode (accountID: %s)", accountID)
        return jsonify(status = "403006", msg = "Wrong VerifyCode")

    conn.UpdateNewEmail(userid, email)

    # Example: Return a response to the user
    return "Email verification successful!"
