-- USE game;


INSERT INTO `account` (`name`, `email`, `password`, `token_id`, `token_validity`, `verify_code`, `expiration_time`, `salt`) VALUES
('John Doe', 'john.doe@example.com', 'password123', 'token123', NOW(), 'verify123', NOW(), 'salt123'),
('Jane Smith', 'jane.smith@example.com', 'password456', 'token456', NOW(), 'verify456', NOW(), 'salt456');


INSERT INTO achievement (achievement_name, achievement_type, achievement_description) VALUES
('Achievement1', 'Type1', 'Description1'),
('Achievement2', 'Type1', 'Description2'),
('Achievement3', 'Type2', 'Description3');


SET FOREIGN_KEY_CHECKS = 0;
truncate table `card_style`;
INSERT INTO `card_style` (`card_style_name`, `card_style_description`, `card_style_probability`) VALUES
('Frozen,Civil', 'Frozen(平民)', 0.0165),
('Frozen,Killer', 'Frozen(殺手)', 0.0167),
('Frozen,King', 'Frozen(國王)', 0.0167),
('Frozen,Knight', 'Frozen(騎士)', 0.0167),
('Frozen,Prince', 'Frozen(王子)', 0.0167),
('Frozen,Queen', 'Frozen(皇后)', 0.0167),
('Aladdin,Civil', 'Aladdin(平民)', 0.0165),
('Aladdin,Killer', 'Aladdin(殺手)', 0.0167),
('Aladdin,King', 'Aladdin(國王)', 0.0167),
('Aladdin,Knight', 'Aladdin(騎士)', 0.0167),
('Aladdin,Prince', 'Aladdin(王子)', 0.0167),
('Aladdin,Queen', 'Aladdin(皇后)', 0.0167),
('wonderland,Civil', 'Wonderland(平民)', 0.0165),
('wonderland,Killer', 'Wonderland(殺手)', 0.0167),
('wonderland,King', 'Wonderland(國王)', 0.0167),
('wonderland,Knight', 'Wonderland(騎士)', 0.0167),
('wonderland,Prince', 'Wonderland(王子)', 0.0167),
('wonderland,Queen', 'Wonderland(皇后)', 0.0167),
('Cinderella,Civil', 'Cinderella(平民)', 0.0165),
('Cinderella,Killer', 'Cinderella(殺手)', 0.0167),
('Cinderella,King', 'Cinderella(國王)', 0.0167),
('Cinderella,Knight', 'Cinderella(騎士)', 0.0167),
('Cinderella,Prince', 'Cinderella(王子)', 0.0167),
('Cinderella,Queen', 'Cinderella(皇后)', 0.0167),
('Romeo and Juliet,Civil', 'Romeo(平民)', 0.0165),
('Romeo and Juliet,Killer', 'Romeo(殺手)', 0.0167),
('Romeo and Juliet,King', 'Romeo(國王)', 0.0167),
('Romeo and Juliet,Knight', 'Romeo(騎士)', 0.0167),
('Romeo and Juliet,Prince', 'Romeo(王子)', 0.0167),
('Romeo and Juliet,Queen', 'Romeo(皇后)', 0.0167),
('chess,Civil', 'Chess(平民)', 0.0165),
('chess,Killer', 'Chess(殺手)', 0.0167),
('chess,King', 'Chess(國王)', 0.0167),
('chess,Knight', 'Chess(騎士)', 0.0167),
('chess,Prince', 'Chess(王子)', 0.0167),
('chess,Queen', 'Chess(皇后)', 0.0167),
('Chinese chess,Civil', 'ChineseChess(平民)', 0.0165),
('Chinese chess,Killer', 'ChineseChess(殺手)', 0.0167),
('Chinese chess,King', 'ChineseChess(國王)', 0.0167),
('Chinese chess,Knight', 'ChineseChess(騎士)', 0.0167),
('Chinese chess,Prince', 'ChineseChess(王子)', 0.0167),
('Chinese chess,Queen', 'ChineseChess(皇后)', 0.0167),
('Japanese chess,Civil', 'JapaneseChess(平民)', 0.0165),
('Japanese chess,Killer', 'JapaneseChess(殺手)', 0.0167),
('Japanese chess,King', 'JapaneseChess(國王)', 0.0167),
('Japanese chess,Knight', 'JapaneseChess(騎士)', 0.0167),
('Japanese chess,Prince', 'JapaneseChess(王子)', 0.0167),
('Japanese chess,Queen', 'JapaneseChess(皇后)', 0.0167),
('Snow White,Civil', 'SnowWhite(平民)', 0.0165),
('Snow White,Killer', 'SnowWhite(殺手)', 0.0167),
('Snow White,King', 'SnowWhite(國王)', 0.0167),
('Snow White,Knight', 'SnowWhite(騎士)', 0.0167),
('Snow White,Prince', 'SnowWhite(王子)', 0.0167),
('Snow White,Queen', 'SnowWhite(皇后)', 0.0167);
SET FOREIGN_KEY_CHECKS = 1;


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



