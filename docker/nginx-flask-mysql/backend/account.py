from flask import current_app
from flask import Blueprint
from flask import jsonify
from flask import request
from db import DBManager
import secrets, string, datetime

account = Blueprint('account', __name__, url_prefix='/account')



conn = None



''' ==============================
account sign up
input
    account name
    account password
    account email
output
    status
        400001 : Success
        403003 : Username has been used
        403004 : Email already registered
        403005 : Password too short
    token ID
============================== '''
@account.route('/signup', methods=['POST'])
def AccountSignUp():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Input
    accountName = request.form.get('Account')
    accountPassword = request.form.get('Password')
    accountEmail = request.form.get('Email')

    current_app.logger.info("accountName: %s", accountName)
    current_app.logger.info("accountPassword: %s", accountPassword)
    current_app.logger.info("accountEmail: %s", accountEmail)

    # # Input validation
    # # there should check if the input are dangerous

    # Account check
    if(conn.AccountExist(accountName)):
        current_app.logger.info("Username has been used: %s", accountName)
        return jsonify(status = "403003", tokenId = "")

    # Email check
    if(conn.EmailExist(accountEmail)):
        # Email already registered
        current_app.logger.info("Email already registered: %s", accountEmail)
        return jsonify(status = "403004", tokenId = "")

    # Password check
    if(len(accountPassword) < 10):
        # Password too short
        current_app.logger.info("Password too short: %s", accountPassword)
        return jsonify(status = "403005", tokenId = "")

    # Set new account
    conn.SetNewAccount(accountName, accountEmail, accountPassword)

    accountId = conn.FindAccountId(accountName)

    # Hash password with salt
    conn.UpdateNewAccountPassword(accountId)

    # Generate token
    alphabet = string.ascii_letters + string.digits
    tokenId = ''.join(secrets.choice(alphabet) for i in range(20))

    # Generate token validity datetime
    now = datetime.datetime.now()
    now += datetime.timedelta(days=3)
    tokenValidity = now.strftime('%Y-%m-%d %H:%M:%S')

    # Set token and validity datetime
    conn.SetTokenIdAndValidity(accountId, tokenId, tokenValidity)

    # Success
    return jsonify(status = "400000", tokenId = tokenId)



''' ==============================
account login
input
    account name
    account password
output
    status
        400000 : Success
        403001 : No such account
        403002 : Wrong password
    token ID
============================== '''
@account.route('/login', methods=['POST'])
def AccountLogin():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Input
    accountName = request.form.get('Account')
    accountPassword = request.form.get('Password')

    current_app.logger.info("accountName: %s", accountName)
    current_app.logger.info("accountPassword: %s", accountPassword)
    current_app.logger.info(request.form.items())

    # Input validation
    # there should check if the input are dangerous

    # Account check
    if(not conn.AccountExist(accountName)):
        # No such account
        return jsonify(status = "403001", tokenId = "")

    # Password check
    accountId = conn.AccountPasswordCheck(accountName, accountPassword)
    current_app.logger.info("accountId: %s", accountId)
    if(accountId == -1):
        # Wrong password
        return jsonify(status = "403002", tokenId = "")

    # Generate token
    alphabet = string.ascii_letters + string.digits
    tokenId = ''.join(secrets.choice(alphabet) for i in range(20))

    # Generate token validity datetime
    now = datetime.datetime.now()
    now += datetime.timedelta(days=3)
    tokenValidity = now.strftime('%Y-%m-%d %H:%M:%S')

    # Set token and validity datetime
    conn.SetTokenIdAndValidity(accountId, tokenId, tokenValidity)

    # Success
    return jsonify(status = "400000", tokenId = tokenId)