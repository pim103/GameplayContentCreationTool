let Weapon = function (weaponId, weaponName, damages, rateOfFire, projSpeed, projLifeTime, modelId, effect) {
    this.weaponId = weaponId;
    this.weaponName = weaponName;
    this.damages = damages;
    this.rateOfFire = rateOfFire;
    this.projSpeed = projSpeed;
    this.projLifeTime = projLifeTime;
    this.modelId = modelId;
    this.effect = effect;
};

Weapon.fromJSON = function (obj) {
    return new Weapon(
        obj.weaponId,
        obj.weaponName,
        obj.damages,
        obj.rateOfFire,
        obj.projSpeed,
        obj.projLifeTime,
        obj.modelId,
        new Effect(
            obj.effect[0].effectId,
            obj.effect[0].effectName,
            obj.effect[0].interval,
            obj.effect[0].lifeTime,
            obj.effect[0].amount,
            obj.effect[0].SpecialEffect
        )
    );
};

Weapon.prototype.toHTML = function (){
    let parent = document.getElementById("list-weapon");
    Weapon.displayWeapon(this, parent);
}

Weapon.displayWeapon = function (obj, parent) {
    let baseElement = document.getElementById("weapon-none").cloneNode(true);
    let element = baseElement.querySelectorAll("#paragraph-weapon")[0];
    element.querySelectorAll("#weapon-name")[0].innerHTML = obj.weaponName;
    element.querySelectorAll("#weapon-damages")[0].innerHTML = obj.damages;
    element.querySelectorAll("#weapon-rate")[0].innerHTML = obj.rateOfFire;
    element.querySelectorAll("#weapon-speed")[0].innerHTML = obj.projSpeed;
    element.querySelectorAll("#weapon-life")[0].innerHTML = obj.projLifeTime;
    element.querySelectorAll("#weapon-model")[0].innerHTML = obj.modelId;
    element.querySelectorAll("#weapon-effect")[0].innerHTML = " - Effect : " + obj.effect.effectId + ";" + obj.effect.effectName  + ";" + obj.effect.interval  + ";" + obj.effect.lifeTime  + ";" + obj.effect.amount + ";" + obj.effect.SpecialEffect;
    baseElement.style.display = "block";
    // Adding delete button
    let deleteBtn = document.createElement("input");
    deleteBtn.type = "button";
    deleteBtn.value = "Delete";
    deleteBtn.onclick = function(){
        deleteWeapon(obj.weaponId);
    };
    element.appendChild(deleteBtn);
    parent.appendChild(baseElement);
}

function addWeapon() {
    let name = document.getElementById("weapon-name-add");
    let damages = document.getElementById("weapon-damages-add");
    let rateOfFire = document.getElementById("weapon-rate-add");
    let projSpeed = document.getElementById("weapon-speed-add");
    let projLifeTime = document.getElementById("weapon-life-add");
    let modelId = document.getElementById("weapon-model-add");
    let effectSelector = document.getElementById("weapon-effect-add");
    let effectId = effectSelector.options[effectSelector.selectedIndex].value;

    console.log(name.value, damages.value, rateOfFire.value, projSpeed.value, projLifeTime.value, modelId.value, effectId.value);

    WeaponServices.add(name.value, damages.value, rateOfFire.value, projSpeed.value, projLifeTime.value, modelId.value, effectId, function (success) {
        if (success){
            alert("Ajout d'arme réussi");
            name.value = "";
            damages.value = "";
            rateOfFire.value = "";
            projSpeed.value = "";
            projLifeTime.value = "";
            modelId.value = "";
        }
        else {
            alert("Erreur : Obtenez le code d'erreur dans la console.")
            console.log(success);
        }

    });
}

function deleteWeapon(id) {
    WeaponServices.delete(id, function (success) {
        if (success){
            alert("Arme supprimée");
        }
        else {
            alert("Erreur : Obtenez le code d'erreur dans la console.")
            console.log(success);
        }
    })
}
