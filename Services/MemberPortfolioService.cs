using StoriesOfInclusion.Models;

namespace StoriesOfInclusion.Services
{
    public class MemberPortfolioService
    {
        private readonly List<MemberPortfolio> _portfolios = new()
        {
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Slug = "iffat-nusaiba",
                FullName = "Iffat Nusaiba",
                Initials = "IN",
                RoleTitle = "Co-Founder & Executive Director",
                SystemRole = "superadmin",
                Email = "nusaibaiffaat3@gmail.com",
                Phone = "+880 1914-015054",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "Inclusive education is not charity—it is an unconditional fundamental right for every child in Bangladesh.",
                BioEn = "Iffat Nusaiba is the Co-Founder and Executive Director of Stories of Inclusion. She spearheads project governance, strategic school partnerships, inclusive curriculum architecture, and stakeholder advocacy across Bangladesh. As an award recipient of the BRAC Amra Notun Young Social Entrepreneurs Fellowship 2026, Iffat champions barrier-free learning tools for neurodivergent and visually impaired primary school students.",
                BioBn = "ইফফাত নূসাইবা 'স্টোরিজ অফ ইনক্লুশন'-এর সহ-প্রতিষ্ঠাতা ও নির্বাহী পরিচালক। তিনি প্রকল্প পরিচালনা, স্ট্র্যাটেজিক স্কুল পার্টনারশিপ এবং দৃষ্টিপ্রতিবন্ধী ও বিশেষ চাহিদাসম্পন্ন শিশুদের প্রাথমিক স্তরের অন্তর্ভুক্তিমূলক পাঠ্যক্রম প্রণয়নে নেতৃত্ব দিচ্ছেন। ব্র্যাক আমরা নতুন ফেলোশিপ ২০২৬ বিজয়ী হিসেবে তিনি বাংলাদেশের প্রতিটি জেলায় সমতাভিত্তিক শিক্ষার সুযোগ সম্প্রসারণে কাজ করছেন।",
                Specialties = new() { "Executive Leadership", "Curriculum Design", "Assistive Learning Advocacy", "BRAC Fellowship 2026" },
                ImpactHighlights = new() { "1,700+ Primary Students Reached", "National BRAC Fellowship Conferred", "12+ School Partnerships Established" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Inclusive Primary Curriculum Guidelines (Class 1-3)",
                        Category = "Curriculum Architecture",
                        Year = "2026",
                        Summary = "Standardized multi-sensory teaching guides for teachers accommodating children with visual and cognitive diverse learning needs.",
                        LinkUrl = "/store",
                        ImpactMetric = "Distributed to 15 pilot schools"
                    },
                    new()
                    {
                        Title = "Stories of Inclusion Magazine - Volume 1 Research",
                        Category = "Editorial Research",
                        Year = "2026",
                        Summary = "Field interviews with inclusive educators, identifying key structural challenges in Bangladeshi mainstream primary schools.",
                        LinkUrl = "/activities/magazine",
                        ImpactMetric = "Featured Editorial in Vol. 1"
                    },
                    new()
                    {
                        Title = "BRAC Fellowship Empathy Session Framework",
                        Category = "Advocacy Workshop",
                        Year = "2026",
                        Summary = "Peer empathy modules delivered in primary classrooms to eliminate social stigma around peer disabilities.",
                        LinkUrl = "/recognition",
                        ImpactMetric = "Conferred BRAC National Award"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Slug = "musarrat-jahan-esha",
                FullName = "Musarrat Jahan Esha",
                Initials = "ME",
                RoleTitle = "Creative Lead & Co-Founder",
                SystemRole = "admin",
                Email = "mjesha12@gmail.com",
                Phone = "+880 1772-838474",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "Accessible visuals and tactile storytelling empower children to see, feel, and dream without limits.",
                BioEn = "Musarrat Jahan Esha leads creative direction, visual brand storytelling, and tactile learning book design at Stories of Inclusion. She oversees high-contrast visual standards, illustrated storytelling characters, and sensory educational materials tailored for early childhood education.",
                BioBn = "মুসাররাত জাহান এশা 'স্টোরিজ অফ ইনক্লুশন'-এর সহ-প্রতিষ্ঠাতা ও ক্রিয়েটিভ লিড। তিনি অন্তর্ভুক্তিমূলক ভিজ্যুয়াল স্টোরিটেলিং, স্পর্শমূলক বইয়ের চিত্রায়ন এবং শিশুদের জন্য উচ্চ বৈসাদৃশ্যযুক্ত ডিজাইন নির্দেশনায় দায়িত্ব পালন করছেন।",
                Specialties = new() { "Creative Direction", "Tactile Book Design", "Visual Storytelling", "High-Contrast Illustrations" },
                ImpactHighlights = new() { "40+ Illustrated Characters", "Tactile Guideline Book Artwork", "WCAG 2.1 Contrast Standards" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "The Inclusive Guideline Book - Tactile Edition",
                        Category = "Tactile Print Design",
                        Year = "2026",
                        Summary = "Illustrated sensory storybook with raised touchlines and high-contrast color palettes for low-vision readers.",
                        LinkUrl = "/store",
                        ImpactMetric = "Official Store Publication"
                    },
                    new()
                    {
                        Title = "Accessible Visual System & Campaign Branding",
                        Category = "Brand Identity",
                        Year = "2026",
                        Summary = "Universal accessible design system maintaining minimum 4.5:1 WCAG contrast across all digital and print assets.",
                        LinkUrl = "/about",
                        ImpactMetric = "Platform Design Standard"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Slug = "shoaib-ahmed",
                FullName = "Shoaib Ahmed",
                Initials = "SA",
                RoleTitle = "Lead Developer & Accessibility Architect",
                SystemRole = "admin",
                Email = "shoaibahmed.5063@gmail.com",
                Phone = "+880 1772-838474",
                Location = "Middle Badda, Dhaka",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "The web was built for everyone. Digital barriers can and must be engineered out of existence.",
                BioEn = "Shoaib Ahmed is the Lead Full-Stack Developer and Digital Accessibility Architect of Stories of Inclusion. He engineered the entire platform architecture using ASP.NET Core Blazor, PostgreSQL/Supabase, and tactile skeuomorphic styling, adhering strictly to WCAG 2.1 AA criteria with real-time screen-reader live regions, dyslexia typography switches, and audio synchronization.",
                BioBn = "শোয়েব আহমেদ 'স্টোরিজ অফ ইনক্লুশন'-এর প্রধান ডেভেলপার ও ডিজিটাল অ্যাক্সেসিবিলিটি আর্কিটেক্ট। তিনি ডব্লিউসিএজি ২.১ এএ স্ট্যান্ডার্ড অনুযায়ী পুরো ওয়েবসাইট, অডিওবুক প্লেয়ার ও আরবিএসি সিস্টেম তৈরি করেছেন।",
                Specialties = new() { "Full-Stack Development", "WCAG 2.1 AA Engineering", "Tactile Skeuomorphic UI", "Blazor & PostgreSQL" },
                ImpactHighlights = new() { "100% WCAG 2.1 AA Audit Pass", "Bilingual Real-Time State Engine", "Zero Emoji Clean Skeuomorphism" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Stories of Inclusion Interactive Web Platform",
                        Category = "Software Engineering",
                        Year = "2026",
                        Summary = "High-performance accessible web platform featuring responsive audiobooks, accessible magazines, galleries, and admin consoles.",
                        LinkUrl = "/",
                        ImpactMetric = "Full Production Platform"
                    },
                    new()
                    {
                        Title = "WCAG 2.1 AA Accessible Audio Player Engine",
                        Category = "Accessibility Tooling",
                        Year = "2026",
                        Summary = "Custom keyboard-controllable audio and YouTube synchronizer with downloadable Bengali and English transcripts.",
                        LinkUrl = "/audiobooks",
                        ImpactMetric = "Zero-barrier audio access"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Slug = "naima-jannat-taki",
                FullName = "Naima Jannat Taki",
                Initials = "NT",
                RoleTitle = "Program Lead & Co-Founder",
                SystemRole = "admin",
                Email = "naima.taki@storiesofinclusion.org",
                Phone = "+880 1914-015054",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "True inclusion begins when teachers and classmates understand each child's unique way of experiencing the world.",
                BioEn = "Naima Jannat Taki is a Co-Founder and Program Lead overseeing offline workshop design, field school outreach, and volunteer management. She coordinates direct educational interventions in partner schools.",
                BioBn = "নাঈমা জান্নাত তাকি সহ-প্রতিষ্ঠাতা ও প্রোগ্রাম লিড। তিনি মাঠপর্যায়ের প্রাথমিক স্কুলগুলোতে অন্তর্ভুক্তিমূলক সচেতনতা কর্মশালা ও স্বেচ্ছাসেবক পরিচালনায় নেতৃত্ব দিচ্ছেন।",
                Specialties = new() { "Field Program Design", "School Outreach", "Volunteer Facilitation", "Community Partnerships" },
                ImpactHighlights = new() { "25+ School Workshops Conducted", "150+ Volunteers Trained", "Gazipur & Dhaka Outreach" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Empathy in Action: School Workshop Series",
                        Category = "Field Workshop",
                        Year = "2026",
                        Summary = "Interactive workshops connecting primary school students with assistive learning devices and audiobook reading circles.",
                        LinkUrl = "/activities",
                        ImpactMetric = "800+ Students Engaged"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Slug = "rafieu-arnob",
                FullName = "Md. Rafieu Hossain Arnob",
                Initials = "RA",
                RoleTitle = "Operations Strategist",
                SystemRole = "admin",
                Email = "rafieu.arnob@storiesofinclusion.org",
                Phone = "+880 1772-838474",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "Scalable educational change requires rigorous operational discipline and transparent logistics.",
                BioEn = "Md. Rafieu Hossain Arnob leads operational efficiency, field resource logistics, and supply chain management for the publication and delivery of tactile learning materials.",
                BioBn = "মো. রাফিউ হোসাইন অর্ণব অপারেশনাল স্ট্র্যাটেজিস্ট হিসেবে প্রকল্পের লজিস্টিকস, সামগ্রী বিতরণ ও রিসোর্স ম্যানেজমেন্ট পরিচালনা করছেন।",
                Specialties = new() { "Operations Management", "Supply Chain & Logistics", "Resource Allocation", "Partnership Scaling" },
                ImpactHighlights = new() { "100% On-Time Material Handover", "District Distribution Framework", "BRAC Fellowship Operations" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Sensory Learning Kit Nationwide Distribution Model",
                        Category = "Logistics Plan",
                        Year = "2026",
                        Summary = "Supply and delivery architecture ensuring free audio and tactile kits reach remote primary schools without transit delay.",
                        LinkUrl = "/store",
                        ImpactMetric = "District logistics pipeline"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Slug = "afnan-orthey",
                FullName = "Afnan Jahan Orthey",
                Initials = "AO",
                RoleTitle = "Co-Founder & Policy Advocate",
                SystemRole = "admin",
                Email = "afnan.orthey@storiesofinclusion.org",
                Phone = "+880 1914-015054",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "Sustainable progress for children with disabilities requires institutional policy backing.",
                BioEn = "Afnan Jahan Orthey is a Co-Founder focused on educational policy advocacy, strategic institutional partnerships, and aligning the organization's goals with Sustainable Development Goals 4.5, 4.8, and 10.2.",
                BioBn = "আফনান জাহান অর্থী সহ-প্রতিষ্ঠাতা ও নীতি বিষয়ক অ্যাডভোকেট। তিনি শিক্ষা নীতি সংস্কার ও আন্তর্জাতিক এসডিজি লক্ষ্যমাত্রা অর্জনে কাজ করছেন।",
                Specialties = new() { "Educational Policy", "SDG Alignment", "Institutional Relations", "Strategic Advocacy" },
                ImpactHighlights = new() { "SDG 4.5 & 10.2 Strategic Framework", "Policy Brief Contributions", "Stakeholder Roundtables" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Inclusive Education Whitepaper: Bangladesh SDG 4.5",
                        Category = "Policy Research",
                        Year = "2026",
                        Summary = "Detailed review on mainstreaming children with visual disabilities into general primary classrooms.",
                        LinkUrl = "/activities/magazine",
                        ImpactMetric = "Published in SOI Magazine Vol. 1"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                Slug = "masruba-tanha",
                FullName = "Masruba Mehejabin Tanha",
                Initials = "MT",
                RoleTitle = "Public Engagement & Content Coordinator",
                SystemRole = "user",
                Email = "masruba.tanha@storiesofinclusion.org",
                Phone = "+880 1772-838474",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "When people hear the real stories of our children, empathy naturally replaces hesitation.",
                BioEn = "Masruba Mehejabin Tanha manages public storytelling campaigns, multimedia video features, student spotlight interviews, and social media outreach.",
                BioBn = "মাসরুবা মেহজাবিন তানহা পাবলিক এনগেজমেন্ট ও কন্টেন্ট কোঅর্ডিনেটর। তিনি সচেতনতামূলক কন্টেন্ট ও সোশ্যাল মিডিয়া ক্যাম্পেইন পরিচালনা করছেন।",
                Specialties = new() { "Content Strategy", "Social Media Campaigns", "Student Storytelling", "Community Engagement" },
                ImpactHighlights = new() { "23.7K+ Digital Impressions", "15+ Video Feature Stories", "Viral Empathy Campaign" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Behind the Pages: Student Voices Series",
                        Category = "Multimedia Storytelling",
                        Year = "2026",
                        Summary = "Video series capturing the firsthand experiences of primary school students reading with audiobooks for the first time.",
                        LinkUrl = "/blog",
                        ImpactMetric = "Over 10,000 views"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                Slug = "ehsan-rakib",
                FullName = "Ehsan Al Rakib Chowdhury",
                Initials = "EC",
                RoleTitle = "Campaign & Outreach Coordinator",
                SystemRole = "user",
                Email = "ehsan.rakib@storiesofinclusion.org",
                Phone = "+880 1914-015054",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "Every school visit gives us an opportunity to touch a child's educational future.",
                BioEn = "Ehsan Al Rakib Chowdhury coordinates field logistics, school visit scheduling, student accessibility trials, and volunteer deployments across districts.",
                BioBn = "এহসান আল রাকিব চৌধুরী ফিল্ড ক্যাম্পেইন ও আউটরিচ সমন্বয়ে কাজ করছেন। তিনি স্কুলে সরাসরি বই বিতরণ ও সচেতনতা কার্যক্রম পরিচালনা করেন।",
                Specialties = new() { "Field Logistics", "School Coordination", "Volunteer Dispatch", "Student Accessibility Testing" },
                ImpactHighlights = new() { "30+ Field Visits Executed", "Direct Accessibility Usability Tests", "Volunteer Field Logistics" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Field Usability Report: Audiobook Playback in Classrooms",
                        Category = "Field Testing",
                        Year = "2026",
                        Summary = "Field evaluation documenting acoustic intelligibility of audiobooks in rural classroom environments.",
                        LinkUrl = "/blog",
                        ImpactMetric = "Improved audio clarity"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                Slug = "kazi-airin",
                FullName = "Kazi Airin Sultana",
                Initials = "KS",
                RoleTitle = "Design & Branding Coordinator",
                SystemRole = "user",
                Email = "kazi.airin@storiesofinclusion.org",
                Phone = "+880 1772-838474",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "Design should welcome every eye, regardless of visual acuity.",
                BioEn = "Kazi Airin Sultana develops educational graphic assets, tactile chapter separators, color blindness-safe iconography, and promotional campaign artwork.",
                BioBn = "কাজী আইরিন সুলতানা শিক্ষামূলক গ্রাফিক ডিজাইন, কালার ব্লাইন্ড-ফ্রেন্ডলি আইকনোগ্রাফি ও প্রচারণামূলক চিত্রায়নে নিয়োজিত।",
                Specialties = new() { "Graphic Design", "Accessible Iconography", "Color Accessibility", "Educational Assets" },
                ImpactHighlights = new() { "Color Blind-Safe Asset Kit", "35+ Campaign Posters", "Skeuomorphic Vector Art" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Color Accessibility & Sensory Iconography Set",
                        Category = "Iconography Design",
                        Year = "2026",
                        Summary = "Comprehensive icon set tested for deuteranopia and protanopia to accompany audiobook and magazine chapters.",
                        LinkUrl = "/activities/gallery",
                        ImpactMetric = "Standardized platform asset"
                    }
                }
            },
            new MemberPortfolio
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Slug = "ismail-polash",
                FullName = "Ismail Hosen Polash",
                Initials = "IP",
                RoleTitle = "Media & Production Coordinator",
                SystemRole = "user",
                Email = "ismail.polash@storiesofinclusion.org",
                Phone = "+880 1772-838474",
                Location = "Dhaka, Bangladesh",
                JoinedYear = 2024,
                IsVerified = true,
                Quote = "Clear, warm audio voice-overs open a universe of imagination for visually impaired children.",
                BioEn = "Ismail Hosen Polash manages the acoustic studio recording sessions, voice narrator auditions, sound engineering, noise reduction, and video post-production for all accessible audiobooks.",
                BioBn = "ইসমাইল হোসেন পলাশ অডিওবুক স্টুডিও রেকর্ডিং, সাউন্ড ইঞ্জিনিয়ারিং, নয়েজ রিডাকশন ও ভিডিও সম্পাদনায় দায়িত্ব পালন করছেন।",
                Specialties = new() { "Audio Engineering", "Voice Narration", "Acoustic Mastering", "Video Production" },
                ImpactHighlights = new() { "17.9+ Hours Audio Mastered", "Broadcast-Quality Audio Standards", "Bilingual Narration Tracks" },
                ContributedWorks = new()
                {
                    new()
                    {
                        Title = "Primary Class 1-3 Audiobook Master Tracks",
                        Category = "Acoustic Audio Mastering",
                        Year = "2026",
                        Summary = "Mastered sound recordings with vocal clarity enhancements designed for low-cost mobile speakers and school classrooms.",
                        LinkUrl = "/audiobooks",
                        ImpactMetric = "100% Broadcast Standard"
                    }
                }
            }
        };

        public List<MemberPortfolio> GetAllMembers() => _portfolios;

        public MemberPortfolio? GetBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return null;
            return _portfolios.FirstOrDefault(m => m.Slug.Equals(slug.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public bool UpdateMemberBio(string slug, string bioEn, string bioBn)
        {
            var member = GetBySlug(slug);
            if (member == null) return false;
            member.BioEn = bioEn;
            member.BioBn = bioBn;
            return true;
        }

        public bool AddContributedWork(string slug, PortfolioWorkItem work)
        {
            var member = GetBySlug(slug);
            if (member == null) return false;
            member.ContributedWorks.Insert(0, work);
            return true;
        }
    }
}
