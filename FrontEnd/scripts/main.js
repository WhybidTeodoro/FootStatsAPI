function loadTeams() {
    const teamsTable = document.getElementById("teamsTable");

    teamsTable.innerHTML = `
        <tr>
            <td>1</td>
            <td>Carregado via função</td>
        </tr>
    `;
}

document.addEventListener("DOMContentLoaded", function () {
    loadTeams();
});