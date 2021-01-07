<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>FYC : Création d'un outil dans le cadre du développement d'un jeu vidéo</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="view/models/Effect.js"></script>
    <script src="view/services/EffectServices.js"></script>
    <script src="view/models/Weapon.js"></script>
    <script src="view/services/WeaponServices.js"></script>
</head>
<body>
    <div id="main-div">
        <div id="list-weapon">
            <h2>Liste des Armes : </h2>
            <div id="weapon-none" style="display: none;" class="weapon-class-list">
                <div id="paragraph-weapon">
                    <p id="weapon-name" class="weapon-style">None name</p>
                    <p id="weapon-damages" class="weapon-style">None damages</p>
                    <p id="weapon-rate" class="weapon-style">None rate</p>
                    <p id="weapon-speed" class="weapon-style">None speed</p>
                    <p id="weapon-life" class="weapon-style">None life</p>
                    <p id="weapon-model" class="weapon-style">None model</p>
                    <p id="weapon-effect" class="weapon-style">None effect</p>
                </div>
            </div>
        </div>
        <div id="list-effect">
            <h2>Liste des Effets : </h2>
            <div id="effect-none" style="display: none;" class="effect-class-list">
                <div id="paragraph-effect">
                    <p id="effect-name" class="effect-style">None name</p>
                    <p id="effect-interval" class="effect-style">None interval</p>
                    <p id="effect-life" class="effect-style">None life</p>
                    <p id="effect-amount" class="effect-style">None amount</p>
                    <p id="effect-special" class="effect-style">None special</p>
                </div>
            </div>
        </div>
        <div id="main-weapon">
            <h2>Ajout d'arme : </h2>
            <div id="input-weapon">
                <label for="weapon-name-add">Name : </label><input id="weapon-name-add" name="weapon-name"  type="text"/>
                <label for="weapon-damages-add">Damages : </label><input id="weapon-damages-add" name="weapon-damages"  type="text">
                <label for="weapon-rate-add">Rate : </label><input id="weapon-rate-add" name="weapon-rate"  type="text">
                <label for="weapon-speed-add">Speed : </label><input id="weapon-speed-add" name="weapon-speed"  type="text">
                <label for="weapon-life-add">Life : </label><input id="weapon-life-add" name="weapon-life" type="text">
                <label for="weapon-model-add">Model : </label><input id="weapon-model-add" name="weapon-model" type="text">
                <label for="weapon-effect-add">Effect : </label><select id="weapon-effect-add" name="weapon-effect">
                <input type="button" value="Add" onclick="addWeapon()">
            </div>
        </div>
        <div id="main-effect">
            <h2>Ajout d'effet : </h2>
            <label for="effect-name-add">Name : </label><input id="effect-name-add" name="effect-name" type="text"/>
            <label for="effect-interval-add">Interval : </label><input id="effect-interval-add" name="effect-interval" type="text"/>
            <label for="effect-life-add">Life Time : </label><input id="effect-life-add" name="effect-life" type="text"/>
            <label for="effect-amount-add">Amount : </label><input id="effect-amount-add" name="effect-amount" type="text"/>
            <label for="effect-special-add">SpecialEffect : </label>
            <select id="effect-special-add">
                <option value="0" selected="selected">Dot</option>
                <option value="1">Heal</option>
                <option value="2">Slow</option>
            </select>

            <input type="button" value="Add" onclick="addEffect()">
        </div>
    </div>
    <style>
        .weapon-style, p{
            margin: auto;
            float: left;
            padding: 5px;
        }
        .weapon-class-list, .effect-class-list {
            width: 100%;
            height: 50px;
        }
    </style>
    <script>
        WeaponServices.list(function (weapon) {
            weapon.map(function (w) {
                console.log(w);
                return w.toHTML();
            });
        });
        EffectServices.list(function (effect) {
            let selectorEffect = document.getElementById("weapon-effect-add");
            let res = effect.map(function (e) {
                console.log(e)
                return e.toHTML(1);
            });
            console.log(res)
            res.forEach(selectorEffect.appendChild.bind(selectorEffect));
        });
        EffectServices.list(function (effect) {
            effect.map(function (e) {
                console.log(e)
                return e.toHTML(2);
            });
        });
    </script>
</body>
</html>