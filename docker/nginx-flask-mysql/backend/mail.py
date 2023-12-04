# mail_manager.py
from flask_mail import Mail, Message
from flask import current_app
import random

mail = Mail()

def send_verification_email(recipient_email):
    # Generate six-digit random code
    random_code = ''.join(random.choices('0123456789', k=6))

    # Email subject
    msg_title = 'SWEGame Verification Code'

    # Sender's email address
    msg_sender = current_app.config['MAIL_USERNAME']

    # Recipient's email address
    msg_recipients = [recipient_email]

    # Email content in HTML format
    msg_body = f"""
        <html>
        <head>
            <style>
                body {{
                    background-color: #ff8c00;  /* 上下橘色背景 */
                    font-family: Arial, sans-serif;
                    padding: 20px;
                    margin: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                }}
                .orange-bg {{
                    background-color: #ff8c00;  /* 橘色背景 */
                    color: #ffffff;  /* 文字顏色 */
                    padding: 10px;
                    text-align: center;
                }}
                .white-bg {{
                    background-color: #ffffff;  /* 白色背景 */
                    color: #000000;  /* 文字顏色 */
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    margin-top: 20px;
                    margin-bottom: 20px;
                }}
                .random-code {{
                    font-size: 36px;
                    color: #ff8c00;  /* 橘色文字 */
                    text-align: center;
                    margin-bottom: 10px;
                }}
                .greeting {{
                    margin-top: 2px;
                    text-align: left;
                }}
                .instruction {{
                    text-align: left;
                    margin-top: -10px;
                    margin-bottom: 5px;
                }}
                .contact {{
                    text-align: center;
                    margin-top: -10px;
                    margin-bottom: 20px;
                }}
            </style>
        </head>
        <body>
            <div class="container">
                <div class="orange-bg">
                    <p>重設密碼驗證碼</p>
                </div>
                <div class="white-bg">
                    <p class="greeting">先生/小姐，您好，您的驗證碼為：</p>
                    <p class="random-code">{random_code}</p>
                    <p class="instruction">請於五分鐘內完成驗證手續</p>
                </div>
                <div class="orange-bg">
                    <p class="contact">若有疑問，請聯絡開發團隊最帥的人</p>
                </div>
            </div>
        </body>
        </html>
    """

    # Create a Message object
    msg = Message(msg_title, sender=msg_sender, recipients=msg_recipients)
    msg.body = msg_body
    msg.html = msg_body

    # Send the email
    mail.send(msg)

    return random_code
