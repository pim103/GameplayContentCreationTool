let EffectServices = function () {};

EffectServices.list = function (callback) {
    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            let json = JSON.parse(xhr.responseText);
            let effect = json.map(function (e) {
                return Effect.fromJSON(e);
            });
            callback(effect);
        }
    }
    xhr.open('GET', "/services/effect/list.php?isWebsite=1");
    xhr.send();
}

EffectServices.delete = function (id, callback) {
    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            callback(xhr.status === 201);
        }
    }
    let params = [
        'id='+id
    ];
    xhr.open('POST', "/services/effect/delete.php");
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.send(params.join('&'));
}

EffectServices.add = function (name, interval, life, amount, special, callback) {
    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function() {
        if(xhr.readyState === 4) {
            console.log(xhr.responseText);
            callback(xhr.status === 201);
        }
    };
    let params = [
        'effectName=' + name,
        'intervalTime=' + interval,
        'lifeTime=' + life,
        'amount=' + amount,
        'specialEffect=' + special
    ];
    xhr.open('POST', "/services/effect/add.php");
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.send(params.join('&'));
}