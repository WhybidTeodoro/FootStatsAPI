const API_BASE_URL = "https://localhost:7198/api";

let currentPage = 1;
let pagesize = 10;

let currentName = "";
let currentSortBy = "name";

async function getTeams() {
    try {

        let url = `${API_BASE_URL}/team?pageNumber=1&pagesize=20 `;

        if(currentName){
            url += `&name=${encodeURIComponent(currentName)}`;
        }

        if(currentSortBy){
            url += `&sortBy=${currentSortBy}`;
        }

        console.log("Url da requisição", url);

        const response = await fetch(url);

        if (!response.ok) {
            throw new Error("Erro ao buscar times na API");
        }

        const data = await response.json();

        console.log("Resposta da API:", data);

        renderTeams(data.items);
        renderPagination(data);

    } catch (error) {
        console.error("Erro na requisição:", error);
    }
}

async function loadTeamPlayers(teamId) {
    
    try{

        showLoadingPlayers();

          console.log("Buscando jogadores do time:", teamId);

          const response = await fetch (`${API_BASE_URL}/team/${teamId}/players?pageNumber=1&pageSize=10`);

          if(!response.ok){
            throw new Error("Erro ao buscar jogadores");
          }

          const data = await response.json();

          console.log("Jogadores recebidos:", data);

          renderPlayers(data.items);
    }catch(error){
        console.error("Erro ao buscar jogadores:", error);
        showLoadingPlayers();
    }
}

function renderPlayers(players){
    
    const section = document.getElementById("teamPlayers");
    const table = document.getElementById("playersTable");

    if(!section || !table) return;

    section.style.display = "block";

    table.innerHTML = "";

    if(!players || players.length === 0){
        table.innerHTML = `
        <tr>
            <td colSpan= "7">Nenhum Jogador Encontrado.</td>
        </tr>
        `;
        return;
    }

    let rows = "";

    players.forEach(player => {
        rows += `
            <tr>
                <td>${player.id}</td>
                <td>${player.name}</td>
                <td>${player.position}</td>
                <td>${player.shirtNumber}</td>
                <td>${player.goals}</td>
                <td>${player.assists}</td>
                <td>${player.matchesPlayed}</td>
            </tr>
        `;
    });

    table.innerHTML = rows;
}


function handleTeamClick(teamId, teamName){
    updateSelectedTeamName(teamName);
    loadTeamPlayers(teamId)
    loadTeamStats(teamId)
}

function renderTeams(teams){

    const table = document.getElementById("teamsTable");

    if(!table) return;

    table.innerHTML = "";

    let rows = "";

    teams.forEach(team => {
        rows += `
            <tr>
                <td>${team.id}</td>
                <td>
                    <a href="#" onClick="handleTeamClick(${team.id}, '${team.name}'); return false;">
                        ${team.name}    
                    </a>                    
                </td>
            </tr>`;
    });

    table.innerHTML = rows;
}

function updateSelectedTeamName(teamName){

    const title = document.getElementById("playersTitle");

    if(!title) return;

    title.innerText = `Jogadores do Time: ${teamName}`;
}

function handleSearch(event){
    event.preventDefault();

    currentName = document.getElementById("teamName").value;
    currentSortBy = document.getElementById("sortBy").value;

    currentPage = 1;

    getTeams();
}

function renderPagination(data){
    const pageInfo = document.getElementById("pageInfo");
    const nextBtn = document.getElementById("nextBtn");

    if(!pageInfo || !nextBtn) return;

    const totalPages = Math.ceil(data.totalCount / data.pageSize);

    pageInfo.innerText = `Página ${data.pageNumber} de ${totalPages}`;

    nextBtn.disabled = data.pageNumber >= totalPages;
}

function renderTeamStats(stats){
    const statsList = document.getElementById("statsList");

    if(!statsList) return;

    statsList.innerHTML = `
        <li>Total de Partidas: ${stats.totalMatches}</li>
        <li>Vitórias: ${stats.wins}</li>
        <li>Derrotas: ${stats.losses}</li>
        <li>Empates: ${stats.draws}</li>
        <li>Gols Pró: ${stats.totalGoalsFor}</li>
        <li>Gols Contra: ${stats.totalGoalsAgainst}</li>
        <li>Saldo de Gols: ${stats.goalDifference}</li>
        `;
}

async function loadTeamStats(teamId){
    try{
        console.log("Buscando stats do time: ", teamId);

        const response = await fetch(`${API_BASE_URL}/Stats/team/${teamId}/stats`);

        if(!response.ok){
            throw new Error("Erro ao buscar estatisticas do time");
        }

        const data = await response.json();

        console.log("stats recebidas", data);

        renderTeamStats(data);
    }
    catch(error){
        console.error("erro ao buscar stats:", error);
    }
}

function showLoadingPlayers(){

    const table = document.getElementById("playersTable");
    const section = document.getElementById("teamPlayers");

    if(!table || !section) return;

    section.style.display = "block";

    table.innerHTML = `
    <tr>
        <td colSpan="7">Carregando Jogadores....</td>
    </tr> 
    `;
}

function showErrorPlayers(){

    const table = document.getElementById("playersTable");

    if(!table) return;

    table.innerHTML = `
    <tr>
        <td colSpan="7">Erro ao carregar jogadores</td>
    </tr>
    `;
}


document.addEventListener("DOMContentLoaded", () =>{
    getTeams();

    const form = document.getElementById("teamForm");

    if(form){
        form.addEventListener("submit", handleSearch);
    }

    const prevBtn = document.getElementById("prevPage");
    const nextBtn = document.getElementById("nextPage");


    if(prevBtn){
        prevBtn.addEventListener("click", () =>{
            if(currentPage > 1){
                currentPage--;
                getTeams();
            }
        });
    }

    if(nextBtn){
        nextBtn.addEventListener("click", () =>{
            currentPage++;
            getTeams();
        });
    }
});