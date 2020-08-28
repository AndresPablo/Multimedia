   public float sensitivity = 100;
   public float loudness = 0;
   private AudioSource _audio;
   void Awake()
   {
       _audio = GetComponent<AudioSource>();
   }
   void Start()
   {
       _audio.clip = Microphone.Start(null, true, 10, 44100);
       _audio.loop = true;
       _audio.mute = false;
       while (!(Microphone.GetPosition(null) > 0)) { }
       _audio.Play();
   }
   void Update()
   {
       loudness = GetAveragedVolume() * sensitivity;
       if(loudness > 1)
       {
          //DO SOMETHING
       }
   }
   float GetAveragedVolume()
   {
       float[] data = new float[256];
       float a = 0;
       _audio.GetOutputData(data, 0);
       foreach (float s in data)
       {
           a += Mathf.Abs(s);
       }
       return a / 256;
   }
