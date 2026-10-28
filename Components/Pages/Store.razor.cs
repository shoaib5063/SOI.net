using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Store : ComponentBase, IDisposable
    {
        public string SelectedCategory { get; set; } = "all";
        public string SearchQuery { get; set; } = string.Empty;
        public string SortOption { get; set; } = "featured";

        public List<CartItem> Cart { get; set; } = new();
        public bool IsCartOpen { get; set; } = false;
        public StoreProduct? SelectedProductModal { get; set; }
        
        public StoreOrder OrderForm { get; set; } = new();
        public bool IsOrderSubmitted { get; set; } = false;
        public StoreOrder? CompletedOrder { get; set; }

        public string? ToastMessage { get; set; }

        public List<StoreProduct> Products { get; set; } = new()
        {
            new StoreProduct
            {
                Id = 1,
                TitleEn = "Amar Shobder Golpo: Tactile & Large Print Book",
                TitleBn = "আমার শব্দের গল্প: স্পর্শমূলক ও বড় অক্ষরের গল্পবই",
                Category = "books",
                CategoryLabelEn = "Books & Braille",
                CategoryLabelBn = "বই ও ব্রেইল",
                PriceBdt = 450,
                PriceUsd = 4.00m,
                BadgeTextEn = "Tactile & Large Print",
                BadgeTextBn = "স্পর্শমূলক ও বড় হরফ",
                ImpactNoteEn = "Each copy funds 2 audio lessons for blind primary students.",
                ImpactNoteBn = "প্রতিটি বইয়ের বিক্রির অর্থে ২টি প্রাথমিক অডিও পাঠ তৈরি হয়।",
                IconEmoji = "📖",
                ImageGradient = "linear-gradient(135deg, #0D9488 0%, #115E59 100%)",
                DescriptionEn = "A sensory storybook engineered with embossed tactile illustrations, high-contrast typography, and audio companion QR codes designed for visually impaired and dyslexic learners.",
                DescriptionBn = "উঁচু-নিচু স্পর্শযোগ্য ছবি, বড় হরফের টেক্সট এবং প্রতিটি পৃষ্ঠায় অডিও শুনার কিউআর কোডসহ দৃষ্টিপ্রতিবন্ধী শিশুদের জন্য বিশেষ গল্পবই।",
                FeaturesEn = new() { "Embossed tactile contours on every illustration", "OpenDyslexic & Hind Siliguri 22pt high-contrast text", "Direct YouTube audio narration QR on every chapter", "Certified child-safe rounded corner cardstock" },
                FeaturesBn = new() { "প্রতিটি অলঙ্করণে আঙুল দিয়ে অনুভবের স্পর্শযোগ্য রেখা", "সহজ পাঠযোগ্য ২২ পয়েন্ট বড় মাপের হরফ", "প্রতি অধ্যায়ে মোবাইল দিয়ে স্ক্যান করে শোনার সুবিধা", "শিশুদের জন্য সম্পূর্ণ নিরাপদ মসৃণ বোর্ডবুক" }
            },
            new StoreProduct
            {
                Id = 2,
                TitleEn = "Embossed Wooden Alphabet & Braille Board",
                TitleBn = "কাঠের বর্ণমালা ও ব্রেইল লার্নিং বোর্ড",
                Category = "kits",
                CategoryLabelEn = "Sensory & Learning Kits",
                CategoryLabelBn = "স্পর্শ ও শিখন কিট",
                PriceBdt = 850,
                PriceUsd = 7.50m,
                BadgeTextEn = "Montessori Inclusive",
                BadgeTextBn = "মন্টেসরি অন্তর্ভুক্তিমূলক",
                ImpactNoteEn = "Provides 1 free tactile kit to an inclusive primary school.",
                ImpactNoteBn = "একটি অন্তর্ভুক্তিমূলক সরকারি প্রাথমিক বিদ্যালয়ে ১টি কিট অনুদান করা হয়।",
                IconEmoji = "🧩",
                ImageGradient = "linear-gradient(135deg, #0284C7 0%, #0369A1 100%)",
                DescriptionEn = "Handcrafted sustainable mahogany board featuring engraved Bengali and English alphabets with corresponding standard 6-dot tactile Braille depressions.",
                DescriptionBn = "টেকসই কাঠের ওপর তৈরি বাংলা ও ইংরেজি বর্ণমালা এবং নিচে স্ট্যান্ডার্ড ৬-ডট ব্রেইল স্পর্শ সংকেত যুক্ত আধুনিক লার্নিং বোর্ড।",
                FeaturesEn = new() { "Dual Bangla & English alphabet tactile layout", "Standard Grade-1 Braille dot tactile mapping", "Natural non-toxic beeswax finish", "Accompanied by auditory tactile guide booklet" },
                FeaturesBn = new() { "বাংলা ও ইংরেজি উভয় বর্ণমালার দ্বৈত স্পর্শ লেআউট", "স্ট্যান্ডার্ড গ্রেড-১ ব্রেইল ডট নকশা", "প্রাকৃতিক ও বিষমুক্ত কাঠের মসৃণ ফিনিশ", "অভিভাবক ও শিক্ষকদের জন্য অডিও নির্দেশিকা সহায়িকা" }
            },
            new StoreProduct
            {
                Id = 3,
                TitleEn = "Official Inclusion Champion Organic T-Shirt",
                TitleBn = "ইনক্লুশন ক্যাম্পেইন অফিশিয়াল অর্গানিক টি-শার্ট",
                Category = "merch",
                CategoryLabelEn = "Awareness Merch",
                CategoryLabelBn = "সচেতনতামূলক মার্চেন্ডাইজ",
                PriceBdt = 550,
                PriceUsd = 5.00m,
                BadgeTextEn = "100% Combed Cotton",
                BadgeTextBn = "১০০% প্রিমিয়াম সুতি",
                ImpactNoteEn = "100% profit sponsors audio mastering for class 1 audiobooks.",
                ImpactNoteBn = "১০০% লভ্যাংশ সরাসরি ১ম শ্রেণির অডিওবুক রেকর্ডিং ও মাস্টারিংয়ে ব্যয় হয়।",
                IconEmoji = "👕",
                ImageGradient = "linear-gradient(135deg, #1E293B 0%, #0F172A 100%)",
                DescriptionEn = "High-comfort unisex navy t-shirt featuring the official Stories of Inclusion 'Diversity in Education' emblem with tactile raised-puff Braille printing.",
                DescriptionBn = "উচ্চমানের প্রি-শ্রিঙ্কড সুতি টি-শার্ট, বুকে স্টোরিজ অফ ইনক্লুশনের প্রতীক ও আঙুল দিয়ে অনুভবযোগ্য ব্রেইল এমবস প্রিন্ট।",
                FeaturesEn = new() { "100% Organic 190 GSM ring-spun breathable cotton", "Raised tactile 3D puff puff-print Braille detail", "Unisex ergonomic regular fit (S to XXL)", "Colorfast and machine-wash resistant" },
                FeaturesBn = new() { "১৯০ জিএসএম প্রিমিয়াম নিঃশ্বাসযোগ্য নরম সুতি কাপড়", "স্পর্শে অনুভবযোগ্য ত্রিমাত্রিক ব্রেইল প্রিন্ট", "ইউনিসেক্স আরামদায়ক ফিটিং (এস থেকে ডাবল এক্সেল)", "রং টেকসই এবং দীর্ঘস্থায়ী" }
            },
            new StoreProduct
            {
                Id = 4,
                TitleEn = "'Inclusion Begins With Empathy' Eco Jute-Tote",
                TitleBn = "‘সহমর্মিতায় অন্তর্ভুক্তির শুরু’ ইকো জুট-টোট ব্যাগ",
                Category = "merch",
                CategoryLabelEn = "Awareness Merch",
                CategoryLabelBn = "সচেতনতামূলক মার্চেন্ডাইজ",
                PriceBdt = 320,
                PriceUsd = 3.00m,
                BadgeTextEn = "Eco-Friendly Jute",
                BadgeTextBn = "পরিবেশবান্ধব পাট ও ক্যানভাস",
                ImpactNoteEn = "Funds 15 student empathy awareness workshop materials.",
                ImpactNoteBn = "১৫ জন শিক্ষার্থীর সহমর্মিতা কর্মশালার স্টাডি ম্যাটেরিয়াল অনুদান দেয়।",
                IconEmoji = "👜",
                ImageGradient = "linear-gradient(135deg, #78350F 0%, #92400E 100%)",
                DescriptionEn = "Handmade heavyweight natural golden jute-canvas tote bag with padded straps. Promotes inclusive education and sustainable livelihood for rural women artisans.",
                DescriptionBn = "গ্রামীণ নারী কারিগরদের হাতে তৈরি পরিবেশবান্ধব সোনালী পাট ও ক্যানভাসের মজবুত ব্যাগ। প্রতিদিনের ব্যবহারে টেকসই ও আড়ম্বরহীন নান্দনিক।",
                FeaturesEn = new() { "Locally sourced biodegradable natural jute canvas", "Reinforced box-stitched cotton shoulder handles", "Spacious 15x16 inch capacity with secure zip", "Screen-printed with non-toxic water-based inks" },
                FeaturesBn = new() { "দেশীয় পরিবেশবান্ধব প্রাকৃতিক পাটের ক্যানভাস", "মজবুত প্যাডেড হাতল ও ভেতরের সুরক্ষামূলক জিপার", "বই, ল্যাপটপ বহনের জন্য সুবিধাজনক বড় মাপ", "নিরাপদ পরিবেশবান্ধব রঙে মুদ্রিত বার্তা" }
            },
            new StoreProduct
            {
                Id = 5,
                TitleEn = "Class 1 & 2 Complete Audio Pen-Drive & Tactile Kit",
                TitleBn = "১ম ও ২য় শ্রেণি সম্পূর্ণ অডিও পেনড্রাইভ ও স্পর্শ কিট",
                Category = "kits",
                CategoryLabelEn = "Sensory & Learning Kits",
                CategoryLabelBn = "স্পর্শ ও শিখন কিট",
                PriceBdt = 650,
                PriceUsd = 6.00m,
                BadgeTextEn = "Offline Ready Kit",
                BadgeTextBn = "অফলাইন লার্নিং কিট",
                ImpactNoteEn = "Enables off-grid visually impaired children to learn without internet.",
                ImpactNoteBn = "ইন্টারনেটবিহীন প্রত্যন্ত অঞ্চলের দৃষ্টিপ্রতিবন্ধী শিশুদের পড়াশোনা সচল রাখে।",
                IconEmoji = "💾",
                ImageGradient = "linear-gradient(135deg, #4338CA 0%, #312E81 100%)",
                DescriptionEn = "A high-speed 16GB USB drive preloaded with 60+ full primary audio lessons in Bangla, accompanied by tactile navigation labels and offline printed accessible guides.",
                DescriptionBn = "১৬ জিবি হাই-স্পিড ইউএসবি পেনড্রাইভ, যাতে ১ম ও ২য় শ্রেণির সকল পাঠ্যবইয়ের স্টুডিও-মাস্টার করা অডিও ও টেক্সট অন্তর্ভুক্ত।",
                FeaturesEn = new() { "Preloaded with 60+ high-fidelity studio lessons", "Works on any TV, sound box, laptop, or OTG mobile", "Braille-labeled physical casing for easy identification", "Lifetime free audio revision updates" },
                FeaturesBn = new() { "৬০টির বেশি স্পষ্ট স্টুডিও অডিও লেসন প্রি-লোড করা", "যেকোনো সাউন্ডবক্স, টিভি ও ওটিজি মোবাইলে সরাসরি চলে", "পেনড্রাইভের গায়ে স্পর্শযোগ্য ব্রেইল পরিচিতি লেবেল", "ভবিষ্যতের নতুন সকল অডিও পাঠ বিনামূল্যে আপডেট সুবিধা" }
            },
            new StoreProduct
            {
                Id = 6,
                TitleEn = "Sponsor a Blind Student's Full School Year Kit",
                TitleBn = "দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীর পূর্ণ বার্ষিক শিক্ষা কিট স্পন্সর",
                Category = "sponsor",
                CategoryLabelEn = "Sponsorship & Impact",
                CategoryLabelBn = "স্পন্সর ও প্রত্যক্ষ অনুদান",
                PriceBdt = 1200,
                PriceUsd = 11.00m,
                BadgeTextEn = "Direct Sponsorship",
                BadgeTextBn = "প্রত্যক্ষ অনুদান ও সার্টিফিকেট",
                ImpactNoteEn = "100% delivered to a verified underprivileged student in rural Bangladesh.",
                ImpactNoteBn = "১০০% কিট পৌঁছানো হয় প্রত্যন্ত সরকারি স্কুলে অধ্যয়নরত দৃষ্টিপ্রতিবন্ধী শিশুর কাছে।",
                IconEmoji = "🎓",
                ImageGradient = "linear-gradient(135deg, #B91C1C 0%, #7F1D1D 100%)",
                DescriptionEn = "Gift a complete inclusive learning package: Tactile Storybook, Audio USB drive, Tactile Alphabet Board, and braille stylus notebook directly delivered in the student's name.",
                DescriptionBn = "একজন শিশুর জন্য একটি সম্পূর্ণ প্যাকেজ: স্পর্শ গল্পবই, অডিও পেনড্রাইভ, ব্রেইল স্লেট ও ব্রেইল খাতা। স্পন্সরের নামে শিশুর কাছে সরাসরি হস্তান্তর করা হয়।",
                FeaturesEn = new() { "Direct hand-delivery through SOI school program", "Personalized digital impact certificate sent to you", "Photo update and student progress report included", "Verified under BRAC Changemaker Award compliance" },
                FeaturesBn = new() { "বিদ্যালয়ে সরাসরি শিশুর হাতে পৌঁছে দেওয়ার নিশ্চয়তা", "স্পন্সরের নামে ডিজিটাল ইমপ্যাক্ট সার্টিফিকেট প্রদান", "ফটো ও শিক্ষার্থীর অগ্রগতির আপডেট প্রতিবেদন প্রদান", "স্বচ্ছ ও যাচাইকৃত সামাজিক উদ্যোগ" }
            }
        };

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        public IEnumerable<StoreProduct> FilteredProducts
        {
            get
            {
                var query = Products.AsEnumerable();

                if (SelectedCategory != "all")
                {
                    query = query.Where(p => p.Category.Equals(SelectedCategory, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(SearchQuery))
                {
                    var q = SearchQuery.Trim().ToLowerInvariant();
                    query = query.Where(p =>
                        p.TitleEn.ToLowerInvariant().Contains(q) ||
                        p.TitleBn.ToLowerInvariant().Contains(q) ||
                        p.DescriptionEn.ToLowerInvariant().Contains(q) ||
                        p.DescriptionBn.ToLowerInvariant().Contains(q) ||
                        p.CategoryLabelEn.ToLowerInvariant().Contains(q));
                }

                return SortOption switch
                {
                    "price-low" => query.OrderBy(p => p.PriceBdt),
                    "price-high" => query.OrderByDescending(p => p.PriceBdt),
                    _ => query.OrderBy(p => p.Id)
                };
            }
        }

        public int CartTotalCount => Cart.Sum(c => c.Quantity);
        public decimal CartSubtotal => Cart.Sum(c => c.SubtotalBdt);
        public decimal DeliveryFee => Cart.Any() ? (CartSubtotal >= 1000 || OrderForm.City == "Dhaka" ? 60 : 120) : 0;
        public decimal GrandTotal => CartSubtotal + DeliveryFee;

        public void AddToCart(StoreProduct product)
        {
            var existing = Cart.FirstOrDefault(c => c.Product.Id == product.Id);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                Cart.Add(new CartItem { Product = product, Quantity = 1 });
            }

            ShowToast(AppState.CurrentLanguage == "bn" 
                ? $"'{product.TitleBn}' ব্যাগে যোগ করা হয়েছে!" 
                : $"Added '{product.TitleEn}' to your bag!");
        }

        public void UpdateQuantity(int productId, int delta)
        {
            var item = Cart.FirstOrDefault(c => c.Product.Id == productId);
            if (item != null)
            {
                item.Quantity += delta;
                if (item.Quantity <= 0)
                {
                    Cart.Remove(item);
                }
            }
        }

        public void RemoveFromCart(int productId)
        {
            Cart.RemoveAll(c => c.Product.Id == productId);
        }

        public void OpenQuickView(StoreProduct product)
        {
            SelectedProductModal = product;
        }

        public void CloseQuickView()
        {
            SelectedProductModal = null;
        }

        public void ToggleCart()
        {
            IsCartOpen = !IsCartOpen;
        }

        public void CloseCart()
        {
            IsCartOpen = false;
        }

        public void SubmitOrder()
        {
            if (!Cart.Any()) return;
            if (string.IsNullOrWhiteSpace(OrderForm.FullName) || string.IsNullOrWhiteSpace(OrderForm.Phone))
            {
                ShowToast(AppState.CurrentLanguage == "bn" 
                    ? "অনুগ্রহ করে আপনার নাম ও ফোন নম্বর পূরণ করুন।" 
                    : "Please fill in your name and phone number.");
                return;
            }

            OrderForm.OrderId = $"SOI-{DateTime.UtcNow:MMdd}-{Random.Shared.Next(1000, 9999)}";
            OrderForm.Items = Cart.Select(c => new CartItem { Product = c.Product, Quantity = c.Quantity }).ToList();
            OrderForm.DeliveryFee = DeliveryFee;
            OrderForm.GrandTotal = GrandTotal;

            CompletedOrder = OrderForm;
            IsOrderSubmitted = true;
            Cart.Clear();
        }

        public void ResetOrder()
        {
            IsOrderSubmitted = false;
            CompletedOrder = null;
            OrderForm = new StoreOrder();
            IsCartOpen = false;
        }

        private void ShowToast(string message)
        {
            ToastMessage = message;
            _ = Task.Run(async () =>
            {
                await Task.Delay(3200);
                ToastMessage = null;
                await InvokeAsync(StateHasChanged);
            });
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
