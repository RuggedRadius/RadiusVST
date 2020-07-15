using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;

public enum WaveType
{
    Sine,
    Square,
    Saw,
    Triangle,
    Noise
}

public enum Note   
{
    C,
    CSharp,
    D,
    DSharp,
    E,
    F,
    FSharp,
    G,
    GSharp,
    A,
    ASharp,
    B,
    NULL
}

namespace VST_Synth
{
    public partial class MainWindow : Window
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;


        Random randy = new Random();

        public MainWindow()
        {
            InitializeComponent();
            InitOsc1();
        }

        /// <summary>
        /// Initialises the first oscillator. Fix this later.
        /// </summary>
        private void InitOsc1()
        {
            foreach (WaveType type in Enum.GetValues(typeof(WaveType)))
            {
                cmbOsc1Type.Items.Add(type);
            }
            cmbOsc1Type.SelectedIndex = 0;
        }

        /// <summary>
        /// Determines note for user input key presses on keyboard.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Note GetNoteFromInput(Key key)
        {
            switch (key)
            {
                // Middle Row
                case Key.A:
                    return Note.C;
                case Key.S:
                    return Note.D;
                case Key.D:
                    return Note.E;
                case Key.F:
                    return Note.F;
                case Key.G:
                    return Note.G;
                case Key.H:
                    return Note.A;
                case Key.J:
                    return Note.B;

                // Top Row
                case Key.W:
                    return Note.CSharp;
                case Key.E:
                    return Note.DSharp;
                case Key.T:
                    return Note.FSharp;
                case Key.Y:
                    return Note.GSharp;
                case Key.U:
                    return Note.ASharp;


                default:
                    return Note.NULL;
            }
        }

        /// <summary>
        /// Determines frequency of a note given the note and octave.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="octave"></param>
        /// <returns></returns>
        private float GetFrequencyFromNote(Note note, int octave)
        {
            switch (note)
            {
                case Note.C:
                    return 16.35f * octave;

                case Note.CSharp:
                    return 17.32f * octave;

                case Note.D:
                    return 18.35f * octave;

                case Note.DSharp:
                    return 19.45f * octave;

                case Note.E:
                    return 20.60f * octave;

                case Note.F:
                    return 21.83f * octave;

                case Note.FSharp:
                    return 23.12f * octave;

                case Note.G:
                    return 24.50f * octave;

                case Note.GSharp:
                    return 25.96f * octave;

                case Note.A:
                    return 27.50f * octave;

                case Note.ASharp:
                    return 29.14f * octave;

                case Note.B:
                    return 30.87f * octave;

                default:
                    return 0f;
            }
        }

        /// <summary>
        /// Creates the sound from the sample array.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="waveType"></param>
        /// <param name="octave"></param>
        public void SynthesizeSound(Key e, WaveType waveType, int octave)
        {
            // Init
            short[] wave = new short[SAMPLE_RATE];
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];

            // Determine note and frequency
            Note note = GetNoteFromInput(e);
            if (note == Note.NULL)
                return;
            float frequency = GetFrequencyFromNote(note, octave);


            // Synthesize sample
            SynthSample(waveType, frequency, wave);

            // Copy buffer of short values to wave ...?
            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));

            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            {
                // Init
                short blockAlign = BITS_PER_SAMPLE / 8;
                int subChunk2Size = SAMPLE_RATE * blockAlign;

                // PCM Chunk
                binaryWriter.Write("RIFF".ToCharArray());
                binaryWriter.Write(36 + subChunk2Size);
                binaryWriter.Write("WAVEfmt ".ToCharArray());
                binaryWriter.Write(16);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write(SAMPLE_RATE);
                binaryWriter.Write(SAMPLE_RATE * blockAlign);
                binaryWriter.Write(blockAlign);
                binaryWriter.Write(BITS_PER_SAMPLE);
                binaryWriter.Write("data".ToCharArray());
                binaryWriter.Write(subChunk2Size);
                binaryWriter.Write(binaryWave);

                // Reset memoryStream position after writing data
                memoryStream.Position = 0;

                // Play sample
                new SoundPlayer(memoryStream).Play();
            }
        }
        
        /// <summary>
        /// Populates a short array for the sample based on the given wavetype.
        /// </summary>
        /// <param name="waveType"></param>
        /// <param name="frequency"></param>
        /// <param name="wave"></param>
        private void SynthSample(WaveType waveType, float frequency, short[] wave)
        {
            int samplesPerWaveLength = (int)(SAMPLE_RATE / frequency);
            short ampStep = (short)((short.MaxValue * 2) / samplesPerWaveLength);
            short tempSample;

            switch (waveType)
            {
                case WaveType.Noise:
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        wave[i] = (short)randy.Next(-short.MaxValue, short.MaxValue);
                    }
                    break;

                case WaveType.Saw:
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        tempSample = -short.MaxValue;
                        for (int j = 0; j < samplesPerWaveLength && i < SAMPLE_RATE; j++)
                        {
                            tempSample += ampStep;
                            wave[i++] = Convert.ToInt16(tempSample);
                        }
                        i--;
                    }
                    break;

                case WaveType.Sine:
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i));
                    }
                    break;

                case WaveType.Square:
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        wave[i] = Convert.ToInt16(short.MaxValue * Math.Sign(Math.Sin((Math.PI * 2 * frequency) / SAMPLE_RATE * i)));
                    }
                    break;

                case WaveType.Triangle:
                    tempSample = -short.MaxValue;
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        if (Math.Abs(tempSample + ampStep) > short.MaxValue)
                        {
                            ampStep = (short)-ampStep;
                        }
                        tempSample += ampStep;
                        wave[i] = Convert.ToInt16(tempSample);
                    }
                    break;
            }
        }

        /// <summary>
        /// Called when a key is pressed in the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wndMain_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Press");
            
            Key pressedKey = e.Key;

            switch (pressedKey)
            {
                default:
                    SynthesizeSound(pressedKey, (WaveType)cmbOsc1Type.SelectedItem, (int)sliderOsc1Octave.Value);
                    break;
            }
        }
    }
}
