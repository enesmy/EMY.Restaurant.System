
function setupIcons() {
    //Dark mode vs Light Mode
    
    const lightSchemeIcon1 = document.querySelector('link#light-scheme-icon-x32');
    const lightSchemeIcon2 = document.querySelector('link#light-scheme-icon-x192');
    const lightSchemeIcon3 = document.querySelector('link#light-scheme-icon-x180');
    const lightSchemeIcon4 = document.querySelector('meta#light-scheme-icon-x270');

    const darkSchemeIcon1 = document.querySelector('link#dark-scheme-iconx32');
    const darkSchemeIcon2 = document.querySelector('link#dark-scheme-iconx192');
    const darkSchemeIcon3 = document.querySelector('link#dark-scheme-iconx180');
    const darkSchemeIcon4 = document.querySelector('meta#dark-scheme-iconx270');

    function setLight() {
        document.head.append(lightSchemeIcon1);
        document.head.append(lightSchemeIcon2);
        document.head.append(lightSchemeIcon3);
        document.head.append(lightSchemeIcon4);
        darkSchemeIcon1.remove();
        darkSchemeIcon2.remove();
        darkSchemeIcon3.remove();
        darkSchemeIcon4.remove();
    }

    function setDark() {
        lightSchemeIcon1.remove();
        lightSchemeIcon2.remove();
        lightSchemeIcon3.remove();
        lightSchemeIcon4.remove();
        document.head.append(darkSchemeIcon1);
        document.head.append(darkSchemeIcon2);
        document.head.append(darkSchemeIcon3);
        document.head.append(darkSchemeIcon4);
    }

    const matcher = window.matchMedia('(prefers-color-scheme:dark)');
    function onUpdate() {
        if (matcher.matches) {
            setDark();
        } else {
            setLight();
        }
    }
    matcher.addListener(onUpdate);
    onUpdate();
}

setupIcons();

function cleanPage() {
    var elements = document.getElementsByTagName('input')
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].getAttribute('type') == 'number')
            elements[i].setAttribute('value', elements[i].getAttribute('value').replace(',', '.'))
    }
    
}

