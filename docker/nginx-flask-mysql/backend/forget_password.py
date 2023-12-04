from flask import current_app
from flask import Blueprint
from flask import jsonify
from flask import request
from db import DBManager
from mail import send_verification_email
import secrets, string, datetime

forget_password = Blueprint('forget_password', __name__, url_prefix='/forget_password')



conn = None
#mail_manager = MailManager(current_app)


''' ==============================
CheckAccountRequest
input
    account
    email
output
    status
        400002 : Success
        403001 : No such account
        403006 : Email & account NOT match
============================== '''
@forget_password.route('/checkaccount', methods=['POST'])
def CheckAccount():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Input
    accountName = request.form.get('Account')
    accountEmail = request.form.get('Email')

    current_app.logger.info("accountName: %s", accountName)
    current_app.logger.info("accountEmail: %s", accountEmail)

    # # Input validation
    # # there should check if the input are dangerous

    # Account check
    if not (conn.AccountExist(accountName)):
        current_app.logger.info("Account doesn't exist: %s", accountName)
        return jsonify(status = "403001", tokenId = "")

    accountID = conn.FindAccountId(accountName)
    current_app.logger.info("accountID: %s", accountID)

    if not (conn.AcountEmailCheck(accountID, accountEmail)):
        # Email already registered
        current_app.logger.info("Email and account not match: %s", accountEmail)
        return jsonify(status = "403006", tokenId = "")

    # Success
    # Call send_verification_email
    random_code = send_verification_email(accountEmail)
    current_app.logger.info("Random code generated: %s", random_code)

    # Generate verifycode validity datetime
    now = datetime.datetime.now()
    now += datetime.timedelta(minutes=5)
    verifycodeValidity = now.strftime('%Y-%m-%d %H:%M:%S')
    conn.SetVerifyCodeAndValidity(accountID, random_code, verifycodeValidity)

    #return random_code
    return jsonify(status = "400002", tokenId = "")



''' ==============================
ChangePassword
input
    account
    VarifyCode
    password
output
    status
        400003 : Success
        403007 : Password too short
        403008 : Password too long
        403009 : Wrong VarifyCode //Maybe we limit how many times user can input
        403010 : VarifyCode Expired
============================== '''
@forget_password.route('/changepassword', methods=['POST'])
def Changepassword():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Input
    accountName = request.form.get('Account')
    varifycode = request.form.get('VarifyCode')
    newpassword = request.form.get('Password')



    current_app.logger.info("accountName: %s", accountName)
    current_app.logger.info("VarifyCode: %s", varifycode)
    current_app.logger.info("Password: %s", newpassword)

    # Input validation
    # there should check if the input are dangerous

    '''# Account check
    if(not conn.AccountExist(accountName)):
        # No such account
        return jsonify(status = "403001", tokenId = "")
    '''

    accountID = conn.FindAccountId(accountName)
    current_app.logger.info("accountID: %s", accountID)

    if (len(newpassword)<10):
        current_app.logger.info("password too short (accountID: %s)", accountID)
        return jsonify(status = "403007", tokenId = "")
    elif (len(newpassword)>50):
        current_app.logger.info("password too long (accountID: %s)", accountID)
        return jsonify(status = "403008", tokenId = "")

    if not (conn.CheckVerifyCode(accountID,varifycode)):
        current_app.logger.info("Wrong VerifyCode (accountID: %s)", accountID)
        return jsonify(status = "403009", tokenId = "")

    expiredtime = conn.GetVerifyCodeExpiredTime(accountID)
    current_app.logger.info("Expired time: %s", expiredtime)

    #expiredtime = datetime.strptime(expiredtime, '%Y-%m-%d %H:%M:%S')
    now = datetime.datetime.now()
    current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
    if (expiredtime == -1 or now > expiredtime):
        return jsonify(status = "403010", tokenId = "")

    #changepassword
    # Hash password with salt
    conn.ReloadChangePassword(accountID, newpassword)


    return jsonify(status = "400003", tokenId = "")
