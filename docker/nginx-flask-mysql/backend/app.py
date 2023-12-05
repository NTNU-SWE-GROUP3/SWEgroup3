from flask import Flask

from flask_mail import Mail
from account import account
from forget_password import forget_password
from user_information import user_information
from card_style import card_style
import logging

server = Flask(__name__)



server.config.update(
    #  gmail
    MAIL_SERVER='smtp.gmail.com',
    MAIL_PORT=587,
    MAIL_USE_TLS=True,
    MAIL_USERNAME='swe3onlinegame@gmail.com',
    MAIL_PASSWORD='xybcnshquazoqoux'
)
mail = Mail(server)
LOGFILE = "app.log"
server.logger.setLevel(logging.DEBUG)
fh = logging.FileHandler(LOGFILE)
fh.setLevel(logging.DEBUG)
formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
fh.setFormatter(formatter)
server.logger.addHandler(fh)

# =================
# test: http GET
# =================
@server.route("/api/unity")
def get():
    message = "Hello Unity"
    # return jsonify(text = message)
    return message



server.register_blueprint(account)
server.register_blueprint(forget_password)
server.register_blueprint(user_information)
server.register_blueprint(card_style)

if __name__ == '__main__':
    # server.run()
    server.run(debug=True,host='0.0.0.0')
