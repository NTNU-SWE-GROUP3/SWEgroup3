from flask import Flask

from account import account

server = Flask(__name__)



# =================
# test: http GET
# =================
@server.route("/api/unity")
def get():
    message = "Hello Unity"
    # return jsonify(text = message)
    return message



server.register_blueprint(account)

if __name__ == '__main__':
    # server.run()
    server.run(debug=True,host='0.0.0.0')
