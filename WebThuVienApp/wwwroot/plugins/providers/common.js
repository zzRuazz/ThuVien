function merge(target, source) {
    mergeValues(target, source)
    return target
}

function IsJsonString(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function mergeValues(target, source) {
    if (typeof (target) === 'object' && typeof (source) === 'object') {
        return mergeObjects(target, source)
    }
    if (Array.isArray(target) && Array.isArray(source)) {
        return mergeArrays(target, source)
    }
    if (source === undefined) {
        return target
    }
    return source
}

function mergeObjects(target, source) {
    Object.keys(source).forEach((key) => {
        const sourceValue = source[key]
        const targetValue = target[key]
        target[key] = mergeValues(targetValue, sourceValue)
    })

    return target
}

function mergeArrays(target, source) {
    source.forEach((value, index) => {
        target[index] = mergeValues(target[index], value)
    })

    return target
}

function onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
}

function clearForm(element) {
    $(element).find("input, textarea").val("");
    $(element).find("select").each((_, element) => {
        element.selectedIndex = 0;
    })
}

function ConvertHalfStringData(str) {
    let leng = str.length;
    let spl = Math.ceil(leng / 2);
    let inc = leng - spl;
    let subStr = str.substr(0, spl);

    for (i = 0; i < inc; i++) {
        subStr += "*";
    }

    return subStr;
}
