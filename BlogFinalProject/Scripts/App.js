const answer = document.querySelector('.table');
answer.addEventListener('click', function (event) {
    if (event.target.id === 'Icon') {
        console.log(event.target.id);
        const tbody = event.target.closest('tbody').nextElementSibling;
        if (tbody.style.display === 'none') {
            tbody.style.display = 'table-row-group';
        } else {
            tbody.style.display = 'none';
        }
    }
});

const comment = document.querySelector('.table');
comment.addEventListener('click', function (event) {
    if (event.target.id === 'IconComment') {
        const tbody = event.target.closest('tbody').nextElementSibling.nextElementSibling;
        if (tbody.style.display === 'none') {
            tbody.style.display = 'table-row-group';
        } else {
            tbody.style.display = 'none';
        }
    }
});