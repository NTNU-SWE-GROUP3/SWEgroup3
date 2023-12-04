INSERT INTO `account` (`name`, `email`, `password`, `token_id`, `token_validity`, `verify_code`, `expiration_time`, `salt`) VALUES
('John Doe', 'john.doe@example.com', 'password123', 'token123', NOW(), 'verify123', NOW(), 'salt123'),
('Jane Smith', 'jane.smith@example.com', 'password456', 'token456', NOW(), 'verify456', NOW(), 'salt456');


INSERT INTO achievement (achievement_name, achievement_type, achievement_description) VALUES
('Achievement1', 'Type1', 'Description1'),
('Achievement2', 'Type1', 'Description2'),
('Achievement3', 'Type2', 'Description3');

INSERT INTO `card_style` (`card_style_name`, `card_style_description`, `card_style_probability`) VALUES
('Style 1', 'Description 1', 0.0165),
('Style 2', 'Description 2', 0.0167),
('Style 3', 'Description 3', 0.0167),
('Style 4', 'Description 4', 0.0167),
('Style 5', 'Description 5', 0.0167),
('Style 6', 'Description 6', 0.0167),
('Style 7', 'Description 7', 0.0165),
('Style 8', 'Description 8', 0.0167),
('Style 9', 'Description 9', 0.0167),
('Style 10', 'Description 10', 0.0167),
('Style 11', 'Description 11', 0.0167),
('Style 12', 'Description 12', 0.0167),
('Style 13', 'Description 13', 0.0165),
('Style 14', 'Description 14', 0.0167),
('Style 15', 'Description 15', 0.0167),
('Style 16', 'Description 16', 0.0167),
('Style 17', 'Description 17', 0.0167),
('Style 18', 'Description 18', 0.0167),
('Style 19', 'Description 19', 0.0165),
('Style 20', 'Description 20', 0.0167),
('Style 21', 'Description 21', 0.0167),
('Style 22', 'Description 22', 0.0167),
('Style 23', 'Description 23', 0.0167),
('Style 24', 'Description 24', 0.0167),
('Style 25', 'Description 25', 0.0165),
('Style 26', 'Description 26', 0.0167),
('Style 27', 'Description 27', 0.0167),
('Style 28', 'Description 28', 0.0167),
('Style 29', 'Description 29', 0.0167),
('Style 30', 'Description 30', 0.0167),
('Style 31', 'Description 31', 0.0165),
('Style 32', 'Description 32', 0.0167),
('Style 33', 'Description 33', 0.0167),
('Style 34', 'Description 34', 0.0167),
('Style 35', 'Description 35', 0.0167),
('Style 36', 'Description 36', 0.0167),
('Style 37', 'Description 37', 0.0165),
('Style 38', 'Description 38', 0.0167),
('Style 39', 'Description 39', 0.0167),
('Style 40', 'Description 40', 0.0167),
('Style 41', 'Description 41', 0.0167),
('Style 42', 'Description 42', 0.0167),
('Style 43', 'Description 43', 0.0165),
('Style 44', 'Description 44', 0.0167),
('Style 45', 'Description 45', 0.0167),
('Style 46', 'Description 46', 0.0167),
('Style 47', 'Description 47', 0.0167),
('Style 48', 'Description 48', 0.0167),
('Style 49', 'Description 49', 0.0165),
('Style 50', 'Description 50', 0.0167),
('Style 51', 'Description 51', 0.0167),
('Style 52', 'Description 52', 0.0167),
('Style 53', 'Description 53', 0.0167),
('Style 54', 'Description 54', 0.0167),
('Style 55', 'Description 55', 0.0165),
('Style 56', 'Description 56', 0.0167),
('Style 57', 'Description 57', 0.0167),
('Style 58', 'Description 58', 0.0167),
('Style 59', 'Description 59', 0.0167),
('Style 60', 'Description 60', 0.0167);


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
(1, 'User1', 10, 1000, 'Gold', 50, 30, 5, 800, 1000000),
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



