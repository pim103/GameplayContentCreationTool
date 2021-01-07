let WeaponServices = function () {};

WeaponServices.list = function (callback) {
    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            let json = JSON.parse(xhr.responseText);
            let weapon = json.map(function (w) {
                return Weapon.fromJSON(w);
            });
            callback(weapon);
        }
    }
    xhr.open('GET', "/services/weapon/list.php?isWebsite=1");
    xhr.send();
}

WeaponServices.delete = function (id, callback) {
    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            callback(xhr.status === 201);
        }
    }
    let params = [
        'id='+id
    ];
    xhr.open('POST', "/services/weapon/delete.php");
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.send(params.join('&'));
}

WeaponServices.add = function (weaponName, damages, rateOfFire, projSpeed, projLifeTime, modelId, effect, callback) {
    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function() {
        if(xhr.readyState === 4) {
            console.log(xhr.responseText);
            callback(xhr.status === 201);
        }
    };
    let params = [
        'weaponName=' + weaponName,
        'damages=' + damages,
        'rateOfFire=' + rateOfFire,
        'projSpeed=' + projSpeed,
        'projLifeTime=' + projLifeTime,
        'modelId=' + modelId,
        'effectId=' + effect
    ];
    xhr.open('POST', "/services/weapon/add.php");
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.send(params.join('&'));
}