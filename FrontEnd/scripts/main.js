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

async function handleCreateTeam(event) {
    
    event.preventDefault();

    const teamNameInput = document.getElementById("createTeamName");

    if(!teamNameInput) return;

    const name = teamNameInput.value.trim();

    if(!name){
        alert("Nome do time é obrigatório");
        return;
    }

    try{

        const response = await fetch(`${API_BASE_URL}/team`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                name: name
            })
        });

        if(!response.ok){
            throw new Error("Erro ao criar o time");
        }

        const createdTeam = await response.json();

        console.log("Time Criado: ", createdTeam);

        teamNameInput.value = "";

        getTeams();
    }catch(Error){
        console.error("Erro:", error);
        alert("Erro ao criar o time");
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

                <td class="actions">
                    <button class="edit-btn",  onClick="updateTeam(${team.id}, '${team.name}')">Editar Nome</button>
                    <button class="delete-btn", onClick="deleteTeam(${team.id})">Excluir</button>
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

    const sortSelect = document.getElementById("sortBy");

    const sortBy = sortSelect ? sortSelect.value : "id";

    console.log("Ordenando..", {sortBy});

    getTeams(sortBy);
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

async function deleteTeam(teamId){

    const confirmDelete = confirm("Tem certeza que quer excluir o time?");
    
    if(!confirmDelete) return;

    try{

        const response = await fetch(`${API_BASE_URL}/team/${teamId}`,{
            method: "DELETE"
        });

        if(!response.ok){
            throw new Error("Erro ao excluir o time");
        }

        console.log("Time exluido com sucesso");

        getTeams();
    } catch (error){
        console.error(error);
        alert("Erro ao excluir o time");
    }
}

async function updateTeam(teamId, currentName) {
    
    const newName = prompt("Digite o novo nome do time:", currentName);

    if (newName === null) return;

    const trimmedName = newName.trim();

    if(!trimmedName){
        alert("Nome não pode ser vazio");
        return;
    }

    try{

        const response = await fetch(`${API_BASE_URL}/team/${teamId}`,{
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                name: trimmedName
            })
        });

        if(!response.ok){
            throw new Error("Erro ao atualizar o time");
        }

        console.log("Time atualizado com sucesso");

        getTeams();

    }catch(error){
        console.error(error);
        alert("Erro ao atualizar o time");
    }
}


document.addEventListener("DOMContentLoaded", () =>{
    getTeams();

    const formTeamSearch = document.getElementById("teamSearch");
    const formTeamCreate = document.getElementById("teamCreate");

    if(formTeamSearch){
        formTeamSearch.addEventListener("submit", handleSearch);
    }

    if(formTeamCreate){
        formTeamCreate.addEventListener("submit", handleCreateTeam);
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