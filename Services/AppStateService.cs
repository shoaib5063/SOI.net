namespace StoriesOfInclusion.Services
{
    public enum AppRole
    {
        Guest,
        User,
        Admin,
        SuperAdmin
    }

    public class AppStateService
    {
        public AppRole CurrentRole { get; private set; } = AppRole.Guest;
        public string CurrentLanguage { get; private set; } = "en"; // "en" or "bn"
        public string Theme { get; private set; } = "default"; // "default", "high-contrast", "dark"
        public bool IsDyslexicFont { get; private set; } = false;
        public int FontScalePercent { get; private set; } = 100;
        public bool IsRbacModalOpen { get; private set; } = false;

        public event Action? OnChange;

        public void SetRole(AppRole role)
        {
            CurrentRole = role;
            NotifyStateChanged();
        }

        public void SetLanguage(string lang)
        {
            if (lang == "en" || lang == "bn")
            {
                CurrentLanguage = lang;
                NotifyStateChanged();
            }
        }

        public void CycleTheme()
        {
            Theme = Theme switch
            {
                "default" => "high-contrast",
                "high-contrast" => "dark",
                _ => "default"
            };
            NotifyStateChanged();
        }

        public void ToggleDyslexicFont()
        {
            IsDyslexicFont = !IsDyslexicFont;
            NotifyStateChanged();
        }

        public void IncreaseFont()
        {
            if (FontScalePercent < 130)
            {
                FontScalePercent += 15;
                NotifyStateChanged();
            }
        }

        public void DecreaseFont()
        {
            if (FontScalePercent > 90)
            {
                FontScalePercent -= 10;
                NotifyStateChanged();
            }
        }

        public void ResetFont()
        {
            FontScalePercent = 100;
            NotifyStateChanged();
        }

        public void OpenRbacModal()
        {
            IsRbacModalOpen = true;
            NotifyStateChanged();
        }

        public void CloseRbacModal()
        {
            IsRbacModalOpen = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public string GetRoleDisplayName(AppRole? role = null)
        {
            var r = role ?? CurrentRole;
            return r switch
            {
                AppRole.SuperAdmin => "SuperAdmin",
                AppRole.Admin => "Staff Admin",
                AppRole.User => "Member (User)",
                _ => "Public Visitor"
            };
        }

        public string T(string key)
        {
            var isBn = CurrentLanguage == "bn";
            return key switch
            {
                "brandName" => isBn ? "স্টোরিজ অফ ইনক্লুশন" : "Stories of Inclusion",
                "tagline" => isBn ? "শিক্ষার মাধ্যমে বৈচিত্র্যের ক্ষমতায়ন" : "Empowering Diversity Through Education",
                "navHome" => isBn ? "নীড়পাতা" : "Home",
                "navAbout" => isBn ? "আমাদের সম্পর্কে" : "About Us",
                "navActivities" => isBn ? "কার্যক্রম" : "Activities",
                "navAudiobooks" => isBn ? "অডিওবুক" : "Audiobooks",
                "navAwards" => isBn ? "স্বীকৃতি" : "Recognition",
                "navTeam" => isBn ? "আমাদের দল" : "Our Team",
                "navStore" => isBn ? "স্টোর" : "Store",
                "navJoin" => isBn ? "যুক্ত হোন" : "Join Us",
                "skipLink" => isBn ? "মূল কন্টেন্টে সরাসরি যান" : "Skip to main content",
                "a11yBarTitle" => isBn ? "অ্যাক্সেসিবিলিটি টুলবার" : "Accessibility Toolbar",
                "contrastHigh" => isBn ? "উচ্চ বৈসাদৃশ্য" : "Contrast",
                "dyslexiaFont" => isBn ? "সহজ পাঠ্য ফন্ট" : "Dyslexia Font",
                "readAloud" => isBn ? "পৃষ্ঠাটি শুনুন" : "Read Aloud",
                "heroBadge" => isBn ? "বাংলাদেশে এসডিজি ৪.৫ • ৪.৮ • ১০.২ অর্জনে নিবেদিত" : "Advocating for SDGs 4.5 • 4.8 • 10.2 in Bangladesh",
                "heroTitle" => isBn ? "প্রতিটি শিশুর জন্য নিশ্চিত হোক সমতাভিত্তিক শিক্ষা" : "Making Education Accessible for Every Child",
                "heroSubtitle" => isBn ? "দৃষ্টিপ্রতিবন্ধী ও বিশেষ চাহিদাসম্পন্ন শিশুদের জন্য বাংলাদেশের প্রথম বিনামূল্যে প্রাথমিক শিক্ষামূলক অডিওবুক এবং অন্তর্ভুক্তিমূলক শিক্ষণ প্ল্যাটফর্ম।" : "Pioneering Bangladesh's first free, accessible audiobook and inclusive learning platform for primary students with disabilities and diverse learning needs.",
                "heroCtaPrimary" => isBn ? "অডিওবুক শুনুন" : "Explore Audiobooks",
                "heroCtaSecondary" => isBn ? "আমাদের সাথে যুক্ত হোন" : "Support Our Mission",
                "homeBreadcrumb" => isBn ? "নীড়পাতা" : "Home",
                "storeHeroTitle" => isBn ? "ইনক্লুসিভ এডুকেশন স্টোর" : "Stories of Inclusion Store",
                "storeHeroDesc" => isBn ? "বই, স্পর্শমূলক শিক্ষণ কিট ও সচেতনতামূলক সামগ্রী। প্রতিটি ক্রয়ের ১০০% অর্থ সরাসরি ব্যয় হয় সুবিধাবঞ্চিত ও প্রতিবন্ধী শিশুদের বিনামূল্যে অডিওবুক ও শিক্ষণ সহায়তায়।" : "Tactile books, sensory learning kits, and awareness merchandise. 100% of proceeds directly fund free audiobooks and school inclusion kits across Bangladesh.",
                _ => key
            };
        }
    }
}
