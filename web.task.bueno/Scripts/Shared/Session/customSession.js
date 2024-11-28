
function loadSession() {
    return localStorage.getItem('EsLogeado');
}


function saveSession(custom) {
    localStorage.setItem('EsLogeado', custom);
}