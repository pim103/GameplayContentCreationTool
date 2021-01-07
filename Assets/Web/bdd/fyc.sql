
--
-- Structure de la table `effect`
--

DROP TABLE IF EXISTS `effect`;
CREATE TABLE IF NOT EXISTS `effect` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `effectName` varchar(30) NOT NULL,
  `intervalTime` float NOT NULL,
  `lifeTime` float NOT NULL,
  `amount` float NOT NULL,
  `specialEffect` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `weapon`
--

DROP TABLE IF EXISTS `weapon`;
CREATE TABLE IF NOT EXISTS `weapon` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `weaponName` varchar(30) NOT NULL,
  `damages` int(11) NOT NULL,
  `rateOfFire` float NOT NULL,
  `projSpeed` int(11) NOT NULL,
  `projLifeTime` int(11) NOT NULL,
  `modelId` int(11) NOT NULL,
  `effect` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_weapon_effect` (`effect`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `weapon`
--
ALTER TABLE `weapon`
  ADD CONSTRAINT `fk_weapon_effect` FOREIGN KEY (`effect`) REFERENCES `effect` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
