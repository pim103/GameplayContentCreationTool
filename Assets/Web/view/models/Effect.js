let Effect = function (effectId, effectName, interval, lifeTime, amount, SpecialEffect) {
    this.effectId = effectId;
    this.effectName = effectName;
    this.interval = interval;
    this.lifeTime = lifeTime;
    this.amount = amount;
    this.SpecialEffect = SpecialEffect;
};

Effect.fromJSON = function (obj) {
    return new Effect(
        obj.effectId,
        obj.effectName,
        obj.interval,
        obj.lifeTime,
        obj.amount,
        obj.SpecialEffect
    );
};

Effect.prototype.toHTML = function (displayType){
    if (displayType === 1){
        let option = document.createElement("option");
        Effect.displaySelector(option, this);
        return option;
    }
    if (displayType === 2){
        let parent = document.getElementById("list-effect");
        Effect.displayEffect(parent, this);
    }
    return null;
};

Effect.displayEffect = function (parent, obj) {
    let baseElement = document.getElementById("effect-none").cloneNode(true);
    let element = baseElement.querySelectorAll("#paragraph-effect")[0];
    element.querySelectorAll("#effect-name")[0].innerHTML = obj.effectName;
    element.querySelectorAll("#effect-interval")[0].innerHTML = obj.interval;
    element.querySelectorAll("#effect-life")[0].innerHTML = obj.lifeTime;
    element.querySelectorAll("#effect-amount")[0].innerHTML = obj.amount;
    element.querySelectorAll("#effect-special")[0].innerHTML = obj.SpecialEffect;
    baseElement.style.display = "block";
    // Adding delete button
    let deleteBtn = document.createElement("input");
    deleteBtn.type = "button";
    deleteBtn.value = "Delete";
    deleteBtn.onclick = function(){
        deleteEffect(obj.effectId);
    };
    element.appendChild(deleteBtn);
    parent.appendChild(baseElement);
};

Effect.displaySelector = function (parent, obj){
    parent.value = obj.effectId;
    parent.text = obj.effectName;
};

function addEffect() {
    let name = document.getElementById("effect-name-add");
    let interval = document.getElementById("effect-interval-add");
    let life = document.getElementById("effect-life-add");
    let amount = document.getElementById("effect-amount-add");
    let special = document.getElementById("effect-special-add");
    let specialId = special.options[special.selectedIndex].value;


    EffectServices.add(name.value, interval.value, life.value, amount.value, specialId, function (success) {
        if (success){
            alert("Ajout d'effet réussi");
            name.value = "";
            interval.value = "";
            life.value = "";
            amount.value = "";
        }
        else {
            alert("Erreur : Obtenez le code d'erreur dans la console.")
            console.log(success);
        }

    });
}

function deleteEffect(id) {
    EffectServices.delete(id, function (success) {
        if (success){
            alert("Effet supprimé");
        }
        else {
            alert("Erreur : Obtenez le code d'erreur dans la console.\nVérifiez que votre effet n'est pas utilisé par une arme.")
            console.log(success);
        }
    })
}
