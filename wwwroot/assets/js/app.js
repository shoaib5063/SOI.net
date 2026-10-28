// Main Application Controller
document.addEventListener("DOMContentLoaded", () => {
  let currentLang = localStorage.getItem('soi_language') || 'en';

  // Apply Language on start
  setLanguage(currentLang);

  // Setup Language Buttons
  document.getElementById('btn-lang-en')?.addEventListener('click', () => setLanguage('en'));
  document.getElementById('btn-lang-bn')?.addEventListener('click', () => setLanguage('bn'));

  // Language switcher function
  function setLanguage(lang) {
    if (!translations[lang]) return;
    currentLang = lang;
    localStorage.setItem('soi_language', lang);
    document.documentElement.setAttribute('lang', lang);

    if (lang === 'bn') {
      document.body.classList.add('font-bengali');
    } else {
      document.body.classList.remove('font-bengali');
    }

    // Update active button state
    document.getElementById('btn-lang-en')?.classList.toggle('active', lang === 'en');
    document.getElementById('btn-lang-bn')?.classList.toggle('active', lang === 'bn');

    // Update all text nodes with data-i18n
    const dict = translations[lang];
    document.querySelectorAll('[data-i18n]').forEach(el => {
      const key = el.getAttribute('data-i18n');
      if (dict[key]) {
        if (el.tagName === 'INPUT' || el.tagName === 'TEXTAREA') {
          el.setAttribute('placeholder', dict[key]);
        } else {
          el.textContent = dict[key];
        }
      }
    });

    // Announce language change
    window.a11yManager?.announce(lang === 'bn' ? "ভাষা পরিবর্তন করে বাংলা করা হয়েছে" : "Language switched to English");
  }

  // Accessible Audio Player Demo Logic
  const audioContext = new (window.AudioContext || window.webkitAudioContext)();
  let activeOscillator = null;
  let isAudioPlaying = false;

  document.querySelectorAll('.play-toggle-btn').forEach(btn => {
    btn.addEventListener('click', (e) => {
      const button = e.currentTarget;
      const card = button.closest('.audiobook-card');
      const progressFill = card.querySelector('.progress-fill');

      if (isAudioPlaying) {
        stopAudioPlayback();
        button.innerHTML = '▶';
        button.setAttribute('aria-label', 'Play lesson audio');
        if (progressFill) progressFill.style.width = '35%';
      } else {
        startAudioPlayback();
        button.innerHTML = '⏸';
        button.setAttribute('aria-label', 'Pause lesson audio');
        if (progressFill) progressFill.style.width = '75%';
      }
    });
  });

  function startAudioPlayback() {
    try {
      if (audioContext.state === 'suspended') audioContext.resume();
      activeOscillator = audioContext.createOscillator();
      const gainNode = audioContext.createGain();
      activeOscillator.type = 'sine';
      activeOscillator.frequency.setValueAtTime(440, audioContext.currentTime); // gentle A4 tone
      gainNode.gain.setValueAtTime(0.05, audioContext.currentTime);
      activeOscillator.connect(gainNode);
      gainNode.connect(audioContext.destination);
      activeOscillator.start();
      isAudioPlaying = true;
      window.a11yManager?.announce("Audio playback started");
    } catch(err) {
      console.log("Audio simulation", err);
    }
  }

  function stopAudioPlayback() {
    if (activeOscillator) {
      try {
        activeOscillator.stop();
        activeOscillator.disconnect();
      } catch(e) {}
      activeOscillator = null;
    }
    isAudioPlaying = false;
    window.a11yManager?.announce("Audio playback paused");
  }

  // Toggle Accessible Transcripts
  document.querySelectorAll('.btn-transcript-toggle').forEach(btn => {
    btn.addEventListener('click', (e) => {
      const card = e.currentTarget.closest('.audiobook-card');
      const transcriptBox = card.querySelector('.transcript-box');
      const isHidden = transcriptBox.style.display === 'none';
      transcriptBox.style.display = isHidden ? 'block' : 'none';
      btn.setAttribute('aria-expanded', isHidden);
      window.a11yManager?.announce(isHidden ? "Transcript expanded" : "Transcript collapsed");
    });
  });

  // Contact / Join Us Form Handler
  const contactForm = document.getElementById('contact-form');
  contactForm?.addEventListener('submit', (e) => {
    e.preventDefault();
    const type = document.getElementById('form-type')?.value;
    const name = document.getElementById('form-name')?.value.trim();
    const email = document.getElementById('form-email')?.value.trim();
    const phone = document.getElementById('form-phone')?.value.trim();
    const message = document.getElementById('form-message')?.value.trim();

    if (!name || !email || !message) {
      alert("Please fill in all required fields.");
      return;
    }

    const newSubmission = {
      id: Date.now(),
      type: type === 'join' ? 'Join Us' : 'Contact Inquiry',
      name: name,
      email: email,
      phone: phone || 'N/A',
      message: message,
      date: new Date().toISOString().split('T')[0]
    };

    const submissions = JSON.parse(localStorage.getItem('soi_submissions') || '[]');
    submissions.unshift(newSubmission);
    localStorage.setItem('soi_submissions', JSON.stringify(submissions));

    // Show accessible success message
    const alertBox = document.getElementById('form-success-alert');
    if (alertBox) {
      alertBox.style.display = 'block';
      alertBox.focus();
      window.a11yManager?.announce("Your message has been submitted successfully!");
    }

    contactForm.reset();
  });
});
