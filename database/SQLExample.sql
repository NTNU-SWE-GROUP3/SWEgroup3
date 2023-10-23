INSERT INTO account (name, email, password, token_id, token_validity, salt) VALUES
('User1', 'user1@example.com', 'password1', 'token1', 3600, 'salt1'),
('User2', 'user2@example.com', 'password2', 'token2', 7200, 'salt2');

INSERT INTO achievement (achievement_name, achievement_type, achievement_description) VALUES
('Achievement1', 'Type1', 'Description1'),
('Achievement2', 'Type1', 'Description2'),
('Achievement3', 'Type2', 'Description3');

INSERT INTO card_style (card_style_name, card_style_description, card_style_probability) VALUES
('Style1', 'Style Description 1', 0.2),
('Style2', 'Style Description 2', 0.3),
('Style3', 'Style Description 3', 0.1);

INSERT INTO skill (skill_name, skill_description, skill_probabiity) VALUES
('Skill1', 'Skill Description 1', 0.4),
('Skill2', 'Skill Description 2', 0.5),
('Skill3', 'Skill Description 3', 0.2);

INSERT INTO account_data (account_id, nickname, level, experience, `rank`, total_match, total_win, ranked_winning_streak, ranked_XP, coin) VALUES
(1, 'User1', 10, 1000, 'Gold', 50, 30, 5, 800, 5000),
(2, 'User2', 8, 800, 'Silver', 45, 25, 3, 700, 4500);

INSERT INTO account_achievement (account_id, achievement_id) VALUES
(1, 1),
(1, 2),
(2, 3);

INSERT INTO account_card_style (account_id, card_style_id) VALUES
(1, 1),
(1, 2),
(2, 3);

INSERT INTO account_skill (account_id, skill_id) VALUES
(1, 1),
(1, 2),
(2, 3);

