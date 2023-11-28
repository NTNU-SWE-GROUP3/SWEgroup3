INSERT INTO `account` (`name`, `email`, `password`, `token_id`, `token_validity`, `verify_code`, `expiration_time`, `salt`) VALUES
('John Doe', 'john.doe@example.com', 'password123', 'token123', NOW(), 'verify123', NOW(), 'salt123'),
('Jane Smith', 'jane.smith@example.com', 'password456', 'token456', NOW(), 'verify456', NOW(), 'salt456');


INSERT INTO achievement (achievement_name, achievement_type, achievement_description) VALUES
('Achievement1', 'Type1', 'Description1'),
('Achievement2', 'Type1', 'Description2'),
('Achievement3', 'Type2', 'Description3');

INSERT INTO `card_style` (`card_style_name`, `card_style_description`, `card_style_probability`) VALUES
('Style 1', 'Description 1', 0.033),
('Style 2', 'Description 2', 0.033),
('Style 3', 'Description 3', 0.033),
('Style 4', 'Description 4', 0.033),
('Style 5', 'Description 5', 0.033),
('Style 6', 'Description 6', 0.033),
('Style 7', 'Description 7', 0.033),
('Style 8', 'Description 8', 0.033),
('Style 9', 'Description 9', 0.033),
('Style 10', 'Description 10', 0.033),
('Style 11', 'Description 11', 0.033),
('Style 12', 'Description 12', 0.033),
('Style 13', 'Description 13', 0.033),
('Style 14', 'Description 14', 0.033),
('Style 15', 'Description 15', 0.033),
('Style 16', 'Description 16', 0.033),
('Style 17', 'Description 17', 0.033),
('Style 18', 'Description 18', 0.033),
('Style 19', 'Description 19', 0.033),
('Style 20', 'Description 20', 0.033),
('Style 21', 'Description 21', 0.033),
('Style 22', 'Description 22', 0.033),
('Style 23', 'Description 23', 0.033),
('Style 24', 'Description 24', 0.033),
('Style 25', 'Description 25', 0.033),
('Style 26', 'Description 26', 0.033),
('Style 27', 'Description 27', 0.033),
('Style 28', 'Description 28', 0.033),
('Style 29', 'Description 29', 0.033),
('Style 30', 'Description 30', 0.034);


INSERT INTO `skill` (`skill_name`, `skill_description`, `skill_probability`) VALUES
('Skill 1', 'Description 1', 0.05),
('Skill 2', 'Description 2', 0.15),
('Skill 3', 'Description 3', 0.1),
('Skill 4', 'Description 4', 0.08),
('Skill 5', 'Description 5', 0.12),
('Skill 6', 'Description 6', 0.07),
('Skill 7', 'Description 7', 0.1),
('Skill 8', 'Description 8', 0.09),
('Skill 9', 'Description 9', 0.1),
('Skill 10', 'Description 10', 0.14);


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



