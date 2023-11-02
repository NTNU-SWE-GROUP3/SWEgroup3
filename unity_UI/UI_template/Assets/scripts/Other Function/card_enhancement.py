from flask import Flask, jsonify
from flask_sqlalchemy import SQLAlchemy

app = Flask(__name__)

# Configure your MySQL database connection
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://username:password@localhost/game'
db = SQLAlchemy(app)

# Define a CardStyle model for the account_card_style table
class CardStyle(db.Model):
    __tablename__ = 'account_card_style'
    id = db.Column(db.Integer, primary_key=True)
    account_id = db.Column(db.Integer, nullable=False)
    card_style_id = db.Column(db.Integer, nullable=False)

# Define a Card model for the card_style table
class Card(db.Model):
    __tablename__ = 'card_style'
    card_style_id = db.Column(db.Integer, primary_key=True)
    card_style_name = db.Column(db.String(255), nullable=False)
    card_style_description = db.Column(db.String(255), nullable=True)

# Create an API endpoint to retrieve card collections for an account
@app.route('/card_collections/<int:account_id>', methods=['GET'])
def get_card_collections(account_id):
    card_collections = db.session.query(Card).join(CardStyle).filter(CardStyle.account_id == account_id).all()
    card_info = [{'id': card.card_style_id, 'name': card.card_style_name, 'description': card.card_style_description} for card in card_collections]
    return jsonify(card_info)

if __name__ == '__main__':
    app.run(debug=True)