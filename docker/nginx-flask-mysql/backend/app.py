from flask import Flask

from flask_mail import Mail
from account import account
from forget_password import forget_password

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

if __name__ == '__main__':
    # server.run()
    server.run(debug=True,host='0.0.0.0')
