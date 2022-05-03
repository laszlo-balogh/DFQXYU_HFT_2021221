let movies = [];
let connection = null;

let movieIdToUpdate = -1;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:47417/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    connection.on("MovieCreated", (user, message) => {
        getdata();
    });
    connection.on("MovieDeleted", (user, message) => {
        getdata();
    });
    connection.on("MovieUpdated", (user, message) => {
        getdata();
    });
    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:47417/Movie')
        .then(x => x.json())
        .then(y => {
            movies = y;
            display();
        });
}

function display() {
    document.getElementById('sajt').innerHTML = "";
    movies.forEach(t => {
        document.getElementById('sajt').innerHTML +=
            "<tr><td>" + t.movieID + "</td><td>" + t.movieTitle + "</td><td>" + t.producer + "</td><td>" + t.location + "</td><td>" + t.year + "</td><td>" + t.price + "</td><td>" +
            `<button onclick="remove(${t.movieID})">Delete</button>` +
            `<button onclick="showupdate(${t.movieID})">Update</button>`
        '</td></tr>';
    });
}

function showupdate(id) {
    document.getElementById('movieTitletoupdate').value = movies.find(t=>t['movieID']==id)['movieTitle']
    document.getElementById('movieProducertoupdate').value = movies.find(t=>t['movieID']==id)['producer']
    document.getElementById('movieLocationtoupdate').value = movies.find(t=>t['movieID']==id)['location']
    document.getElementById('movieYeartoupdate').value = movies.find(t=>t['movieID']==id)['year']
    document.getElementById('moviePricetoupdate').value = movies.find(t=>t['movieID']==id)['price']
    document.getElementById('updateformdiv').style.display = 'inline'
    movieIdToUpdate = id;
}

function create() {
    let mTitle = document.getElementById('movieTitle').value;
    let mProducer = document.getElementById('movieProducer').value;
    let mLocation = document.getElementById('movieLocation').value;
    let mYear = document.getElementById('movieYear').value;
    let mPrice = document.getElementById('moviePrice').value;

    fetch('http://localhost:47417/movie', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                movieTitle: mTitle,
                producer: mProducer,
                location: mLocation,
                year: mYear,
                price: mPrice,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let mTitle = document.getElementById('movieTitletoupdate').value;
    let mProducer = document.getElementById('movieProducertoupdate').value;
    let mLocation = document.getElementById('movieLocationtoupdate').value;
    let mYear = document.getElementById('movieYeartoupdate').value;
    let mPrice = document.getElementById('moviePricetoupdate').value;

    fetch('http://localhost:47417/movie', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                movieId: movieIdToUpdate,
                movieTitle: mTitle,
                producer: mProducer,
                location: mLocation,
                year: mYear,
                price: mPrice,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}


function remove(id) {   
    fetch('http://localhost:47417/Movie/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}
