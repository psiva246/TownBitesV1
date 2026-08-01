$(function () {

    $('.datatable').DataTable({
        responsive: true,
        pageLength: 10,
        autoWidth: false
    });

});

setTimeout(function () {

    $('.alert').fadeOut();

}, 3000);

document.addEventListener('submit', function (e) {
    const form = e.target;

    if (form.classList.contains('delete-form')) {
        e.preventDefault();

        Swal.fire({
            title: 'Delete this record?',
            text: 'This action cannot be undone.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Delete'
        }).then(result => {
            if (result.isConfirmed) {
                form.submit();
            }
        });
    }
});