// Accessibility Engine (WCAG 2.1 Level AA Compliance)
class AccessibilityManager {
  constructor() {
    this.fontScales = [90, 100, 115, 130];
    this.currentScaleIndex = 1; // 100%
    this.isHighContrast = false;
    this.isDyslexicFont = false;
    this.init();
  }

  init() {
    try {
      const savedScale = localStorage.getItem('soi_font_scale');
      if (savedScale !== null) {
        this.currentScaleIndex = parseInt(savedScale, 10);
        this.applyFontScale();
      }

      const savedTheme = localStorage.getItem('soi_contrast_theme');
      if (savedTheme) {
        document.documentElement.setAttribute('data-theme', savedTheme);
      }

      const savedDyslexia = localStorage.getItem('soi_dyslexic_font');
      if (savedDyslexia === 'true') {
        this.isDyslexicFont = true;
        document.body.classList.add('dyslexic-font');
      }
    } catch(e) {}
  }

  increaseFontSize() {
    if (this.currentScaleIndex < this.fontScales.length - 1) {
      this.currentScaleIndex++;
      this.applyFontScale();
    }
  }

  decreaseFontSize() {
    if (this.currentScaleIndex > 0) {
      this.currentScaleIndex--;
      this.applyFontScale();
    }
  }

  resetFontSize() {
    this.currentScaleIndex = 1; // 100%
    this.applyFontScale();
  }

  applyFontScale() {
    const scale = this.fontScales[this.currentScaleIndex] || 100;
    document.documentElement.style.setProperty('--font-scale', `${scale}%`);
    try { localStorage.setItem('soi_font_scale', this.currentScaleIndex); } catch(e){}
    this.announce(`Text size set to ${scale} percent`);
  }

  cycleContrast() {
    const currentTheme = document.documentElement.getAttribute('data-theme') || 'default';
    let nextTheme = 'default';
    if (currentTheme === 'default') nextTheme = 'high-contrast';
    else if (currentTheme === 'high-contrast') nextTheme = 'dark';
    else nextTheme = 'default';

    document.documentElement.setAttribute('data-theme', nextTheme);
    try { localStorage.setItem('soi_contrast_theme', nextTheme); } catch(e){}
    this.announce(`Visual contrast theme set to ${nextTheme}`);
  }

  toggleDyslexicFont() {
    this.isDyslexicFont = !this.isDyslexicFont;
    document.body.classList.toggle('dyslexic-font', this.isDyslexicFont);
    try { localStorage.setItem('soi_dyslexic_font', this.isDyslexicFont); } catch(e){}
    this.announce(this.isDyslexicFont ? "Dyslexia-friendly font enabled" : "Standard font enabled");
  }

  toggleReadAloud(lang) {
    return window.soiApp.toggleReadAloud(lang);
  }

  announce(message) {
    const liveRegion = document.getElementById('a11y-live-region');
    if (liveRegion) {
      liveRegion.textContent = '';
      setTimeout(() => {
        liveRegion.textContent = message;
      }, 50);
    }
  }
}

window.a11yManager = new AccessibilityManager();

let lastReadToggleTimestamp = 0;

window.soiApp = {
  isReading: false,
  currentUtterance: null,

  applyTheme: function(theme) {
    if (!theme) theme = 'default';
    document.documentElement.setAttribute('data-theme', theme);
    try { localStorage.setItem('soi_contrast_theme', theme); } catch(e){}
  },

  setFontScale: function(percent) {
    if (!percent) percent = 100;
    document.documentElement.style.setProperty('--font-scale', percent + '%');
    try { localStorage.setItem('soi_font_scale', percent); } catch(e){}
  },

  setDyslexic: function(enabled) {
    document.body.classList.toggle('dyslexic-font', !!enabled);
    try { localStorage.setItem('soi_dyslexic_font', !!enabled); } catch(e){}
  },

  setLanguage: function(lang) {
    if (!lang) lang = 'en';
    document.documentElement.setAttribute('lang', lang);
    if (lang === 'bn') {
      document.body.classList.add('font-bengali');
    } else {
      document.body.classList.remove('font-bengali');
    }
    try { localStorage.setItem('soi_lang', lang); } catch(e){}
  },

  openRbacModal: function() {
    const modal = document.getElementById('rbac-modal');
    if (modal) {
      modal.classList.add('active');
      modal.style.display = 'flex';
      const dialog = modal.querySelector('.modal-dialog');
      if (dialog) dialog.focus();
    }
  },

  closeRbacModal: function() {
    const modal = document.getElementById('rbac-modal');
    if (modal) {
      modal.classList.remove('active');
      modal.style.display = 'none';
      const openBtn = document.getElementById('btn-open-rbac-modal');
      if (openBtn) openBtn.focus();
    }
  },

  toggleReadAloud: function(lang) {
    const now = Date.now();
    // Guard against multi-firing within 350ms
    if (now - lastReadToggleTimestamp < 350) {
      return this.isReading;
    }
    lastReadToggleTimestamp = now;

    if (!('speechSynthesis' in window)) {
      alert("Text-to-speech is not supported on this browser.");
      return false;
    }

    // If currently speaking or reading, cancel immediately
    if (this.isReading || window.speechSynthesis.speaking) {
      window.speechSynthesis.cancel();
      this.isReading = false;
      this.updateReadBtn(false);
      return false;
    }

    // Always clear any previous queued speech
    window.speechSynthesis.cancel();

    const currentLang = lang || document.documentElement.getAttribute('lang') || 'en';

    // Gather readable text from main content
    const main = document.getElementById('main-content') || document.body;
    const elements = main.querySelectorAll('h1, h2, h3, h4, p');
    let extracted = [];
    elements.forEach(el => {
      if (el.closest('.sr-only, .a11y-bar, .site-header, .site-footer, script, style')) return;
      const txt = (el.innerText || el.textContent || '').trim();
      if (txt.length > 5 && !extracted.includes(txt)) {
        extracted.push(txt);
      }
    });

    let textToRead = extracted.slice(0, 15).join(". ");
    if (!textToRead || textToRead.length < 5) {
      textToRead = (currentLang === 'bn')
        ? "স্টোরিজ অফ ইনক্লুশন। প্রতিটি শিশুর জন্য নিশ্চিত হোক সমতাভিত্তিক শিক্ষা। দৃষ্টিপ্রতিবন্ধী ও বিশেষ চাহিদাসম্পন্ন শিশুদের জন্য বাংলাদেশের প্রথম বিনামূল্যে প্রাথমিক শিক্ষামূলক অডিওবুক এবং অন্তর্ভুক্তিমূলক শিক্ষণ প্ল্যাটফর্ম।"
        : "Stories of Inclusion. Making Education Accessible for Every Child. Pioneering Bangladesh's first free, accessible audiobook and inclusive learning platform for primary students with disabilities and diverse learning needs.";
    }

    this.currentUtterance = new SpeechSynthesisUtterance(textToRead);
    this.currentUtterance.rate = 0.95;
    this.currentUtterance.pitch = 1.0;

    // Pick best available voice
    const voices = window.speechSynthesis.getVoices() || [];
    if (currentLang === 'bn') {
      this.currentUtterance.lang = 'bn-BD';
      const bnVoice = voices.find(v => v.lang && v.lang.startsWith('bn'));
      if (bnVoice) this.currentUtterance.voice = bnVoice;
    } else {
      this.currentUtterance.lang = 'en-US';
      const enVoice = voices.find(v => v.lang && v.lang.startsWith('en'));
      if (enVoice) this.currentUtterance.voice = enVoice;
    }

    this.currentUtterance.onstart = () => {
      window.soiApp.isReading = true;
      window.soiApp.updateReadBtn(true);
    };

    this.currentUtterance.onend = () => {
      window.soiApp.isReading = false;
      window.soiApp.updateReadBtn(false);
    };

    this.currentUtterance.onerror = (e) => {
      window.soiApp.isReading = false;
      window.soiApp.updateReadBtn(false);
    };

    window.speechSynthesis.speak(this.currentUtterance);
    this.isReading = true;
    this.updateReadBtn(true);
    return true;
  },

  updateReadBtn: function(isPlaying) {
    const btn = document.getElementById('btn-read-aloud');
    if (!btn) return;
    const currentLang = document.documentElement.getAttribute('lang') || 'en';
    const stopText = (currentLang === 'bn') ? "থামান" : "Stop";
    const playText = (currentLang === 'bn') ? "পৃষ্ঠাটি শুনুন" : "Read Aloud";
    btn.innerHTML = isPlaying
      ? `<span aria-hidden="true">⏹️</span> <span>${stopText}</span>`
      : `<span aria-hidden="true">🔊</span> <span>${playText}</span>`;
    btn.setAttribute('aria-pressed', isPlaying ? 'true' : 'false');
    btn.classList.toggle('reading-active', isPlaying);
  }
};

// Delegated click listeners on document
document.addEventListener('click', function(e) {
  const decBtn = e.target.closest('#btn-font-dec');
  if (decBtn) {
    window.a11yManager.decreaseFontSize();
    return;
  }
  const incBtn = e.target.closest('#btn-font-inc');
  if (incBtn) {
    window.a11yManager.increaseFontSize();
    return;
  }
  const resetBtn = e.target.closest('#btn-font-reset');
  if (resetBtn) {
    window.a11yManager.resetFontSize();
    return;
  }
  const contrastBtn = e.target.closest('#btn-contrast-toggle');
  if (contrastBtn) {
    window.a11yManager.cycleContrast();
    return;
  }
  const dyslexiaBtn = e.target.closest('#btn-dyslexia-toggle');
  if (dyslexiaBtn) {
    window.a11yManager.toggleDyslexicFont();
    return;
  }
  const langEn = e.target.closest('#btn-lang-en');
  if (langEn) {
    window.soiApp.setLanguage('en');
    return;
  }
  const langBn = e.target.closest('#btn-lang-bn');
  if (langBn) {
    window.soiApp.setLanguage('bn');
    return;
  }
  const openModal = e.target.closest('#btn-open-rbac-modal');
  if (openModal) {
    window.soiApp.openRbacModal();
    return;
  }
  const closeModal = e.target.closest('#btn-close-rbac-modal, .modal-close-btn');
  if (closeModal) {
    window.soiApp.closeRbacModal();
    return;
  }
  const modal = document.getElementById('rbac-modal');
  if (modal && e.target === modal) {
    window.soiApp.closeRbacModal();
    return;
  }
});

document.addEventListener('keydown', function(e) {
  if (e.key === 'Escape') {
    window.soiApp.closeRbacModal();
  }
});

window.addEventListener('beforeunload', () => {
  if (window.speechSynthesis) window.speechSynthesis.cancel();
});
