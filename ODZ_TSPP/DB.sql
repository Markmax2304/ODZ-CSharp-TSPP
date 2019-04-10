-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Мар 28 2019 г., 19:28
-- Версия сервера: 5.7.23
-- Версия PHP: 7.2.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `knigi`
--

-- --------------------------------------------------------

--
-- Структура таблицы `kniga`
--

CREATE TABLE `kniga` (
  `name` varchar(100) NOT NULL,
  `price` int(100) NOT NULL,
  `count` int(100) NOT NULL,
  `confines` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `kniga`
--

INSERT INTO `kniga` (`name`, `price`, `count`, `confines`) VALUES
('Телепузики', 13, 132, '0-3'),
('Узники зоопарка', 61, 2, '0-6'),
('Маша и Медведь', 5, 220, '0-5'),
('Президенте', 41, 12, '21-91'),
('Сыны капитана Гранта', 62, 16, '0-18'),
('Джонни и друзья на деревьях', 48, 56, '0-21'),
('Робинсон Фьюзо', 12, 126, '0-16'),
('Синяя метка', 14, 91, '0-15'),
('Остаться живым', 9, 96, '0-21'),
('451 грудус по Цельсию', 52, 69, '10-21'),
('Мастер и Мандарина', 92, 1, '10-91'),
('Пролетая над гнездом воробья', 100, 1, '18-91'),
('Вино из тюльпанов', 15, 186, '18-91'),
('Анна Гаренина', 14, 6, '18-91'),
('Над пропастью моржи', 45, 9, '0-21'),
('Граф Монте-Карло', 74, 15, '0-21'),
('Евгений Есений', 13, 13, '0-5'),
('Капальни царя Саломона', 15, 14, '0-21'),
('Игра тронов', 19, 13, '0-21'),
('Разнесенные ветром', 15, 11, '0-21'),
('Оловяный ковбой', 18, 3, '0-5'),
('Крестный брат', 12, 16, '21-91'),
('Пепе длинный носок', 21, 132, '0-16'),
('Кот да Винчи', 9, 312, '18-91'),
('Старик и Озеро', 21, 5, '16-18'),
('Обед на обочине', 13, 3, '16-91'),
('Гауст', 1, 421, '18-91'),
('Трое в лодке, не считая кота', 51, 76, '16-91');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
