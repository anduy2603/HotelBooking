// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Date picker initialization
document.addEventListener('DOMContentLoaded', function() {
    // Initialize date pickers
    const dateInputs = document.querySelectorAll('input[type="date"]');
    dateInputs.forEach(input => {
        const today = new Date().toISOString().split('T')[0];
        input.min = today;
    });

    // Room search functionality
    const searchInput = document.querySelector('input[placeholder="Search rooms..."]');
    if (searchInput) {
        searchInput.addEventListener('input', function(e) {
            const searchTerm = e.target.value.toLowerCase();
            const roomCards = document.querySelectorAll('.room-card');
            
            roomCards.forEach(card => {
                const title = card.querySelector('.card-title').textContent.toLowerCase();
                const description = card.querySelector('.card-text').textContent.toLowerCase();
                
                if (title.includes(searchTerm) || description.includes(searchTerm)) {
                    card.style.display = 'block';
                } else {
                    card.style.display = 'none';
                }
            });
        });
    }

    // Room type filter
    const roomTypeSelect = document.querySelector('select.form-select');
    if (roomTypeSelect) {
        roomTypeSelect.addEventListener('change', function(e) {
            const selectedType = e.target.value.toLowerCase();
            const roomCards = document.querySelectorAll('.room-card');
            
            roomCards.forEach(card => {
                const title = card.querySelector('.card-title').textContent.toLowerCase();
                
                if (selectedType === '' || title.includes(selectedType)) {
                    card.style.display = 'block';
                } else {
                    card.style.display = 'none';
                }
            });
        });
    }

    // Price range filter
    const minPriceInput = document.querySelector('input[placeholder="Min"]');
    const maxPriceInput = document.querySelector('input[placeholder="Max"]');
    
    if (minPriceInput && maxPriceInput) {
        const applyPriceFilter = () => {
            const minPrice = parseFloat(minPriceInput.value) || 0;
            const maxPrice = parseFloat(maxPriceInput.value) || Infinity;
            
            const roomCards = document.querySelectorAll('.room-card');
            roomCards.forEach(card => {
                const priceText = card.querySelector('.room-price').textContent;
                const price = parseFloat(priceText.replace(/[^0-9.]/g, ''));
                
                if (price >= minPrice && price <= maxPrice) {
                    card.style.display = 'block';
                } else {
                    card.style.display = 'none';
                }
            });
        };

        minPriceInput.addEventListener('input', applyPriceFilter);
        maxPriceInput.addEventListener('input', applyPriceFilter);
    }

    // Booking form validation
    const bookingForm = document.querySelector('.booking-form form');
    if (bookingForm) {
        bookingForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            // Basic form validation
            const requiredFields = bookingForm.querySelectorAll('[required]');
            let isValid = true;
            
            requiredFields.forEach(field => {
                if (!field.value.trim()) {
                    isValid = false;
                    field.classList.add('is-invalid');
                } else {
                    field.classList.remove('is-invalid');
                }
            });
            
            if (isValid) {
                // Here you would typically send the form data to the server
                alert('Booking submitted successfully!');
                bookingForm.reset();
            }
        });
    }

    // Check-in/out form validation
    const checkInForm = document.querySelector('.card-title:contains("Quick Check-in")').closest('form');
    const checkOutForm = document.querySelector('.card-title:contains("Quick Check-out")').closest('form');
    
    if (checkInForm) {
        checkInForm.addEventListener('submit', function(e) {
            e.preventDefault();
            // Add check-in logic here
            alert('Check-in processed successfully!');
            checkInForm.reset();
        });
    }
    
    if (checkOutForm) {
        checkOutForm.addEventListener('submit', function(e) {
            e.preventDefault();
            // Add check-out logic here
            alert('Check-out processed successfully!');
            checkOutForm.reset();
        });
    }
});
