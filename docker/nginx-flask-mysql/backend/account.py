from flask import current_app
from flask import Blueprint
from flask import jsonify
from flask import request
from db import DBManager
import secrets, string, datetime

account = Blueprint('account', __name__, url_prefix='/account')



conn = None



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