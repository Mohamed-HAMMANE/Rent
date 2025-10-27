import 'package:flutter/material.dart';
import 'package:rent/edit_apartment_screen.dart';
import 'package:rent/full_screen_image_screen.dart';
import 'package:rent/models/apartment.dart';
import 'package:rent/services/api_service.dart';
import 'package:url_launcher/url_launcher.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: const MyHomePage(title: 'Apartments'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  late Future<List<Apartment>> _apartments;

  @override
  void initState() {
    super.initState();
    _apartments = ApiService().getApartments();
  }

  void _refreshApartments() {
    setState(() {
      _apartments = ApiService().getApartments();
    });
  }

  Future<void> _launchUrl(String? url) async {
    if (url != null && await canLaunchUrl(Uri.parse(url))) {
      await launchUrl(Uri.parse(url));
    } else {
      // Handle the error gracefully
      print('Could not launch $url');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
        backgroundColor: Theme.of(context).colorScheme.primary,
        foregroundColor: Theme.of(context).colorScheme.onPrimary,
      ),
      body: FutureBuilder<List<Apartment>>(
        future: _apartments,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(
              child: Text('Failed to load apartments: ${snapshot.error}'),
            );
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return const Center(
              child: Text('No apartments found.'),
            );
          }

          // Sort apartments by mark in descending order
          snapshot.data!.sort((a, b) => (b.mark ?? 0).compareTo(a.mark ?? 0));
          final apartments = snapshot.data!;

          return ListView.builder(
            itemCount: apartments.length,
            itemBuilder: (context, index) {
              final apartment = apartments[index];
              return Card(
                margin:
                    const EdgeInsets.symmetric(horizontal: 12.0, vertical: 6.0),
                clipBehavior: Clip.antiAlias,
                elevation: 3,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    if (apartment.id != null)
                      GestureDetector(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => FullScreenImageScreen(
                                imageUrl:
                                    'http://mohamed.homeip.net:8055/bachelorpad/${apartment.id}/image',
                              ),
                            ),
                          );
                        },
                        child: Stack(
                          alignment: Alignment.topRight,
                          children: [
                            SizedBox(
                              height: 200,
                              width: double.infinity,
                              child: Image.network(
                                'http://mohamed.homeip.net:8055/bachelorpad/${apartment.id}/image',
                                fit: BoxFit.cover,
                                loadingBuilder: (BuildContext context,
                                    Widget child,
                                    ImageChunkEvent? loadingProgress) {
                                  if (loadingProgress == null) return child;
                                  return Center(
                                    child: CircularProgressIndicator(
                                      value: loadingProgress
                                                  .expectedTotalBytes !=
                                              null
                                          ? loadingProgress
                                                  .cumulativeBytesLoaded /
                                              loadingProgress.expectedTotalBytes!
                                          : null,
                                    ),
                                  );
                                },
                                errorBuilder: (context, error, stackTrace) {
                                  return const Center(
                                      child: Icon(Icons.broken_image,
                                          size: 40, color: Colors.grey));
                                },
                              ),
                            ),
                            if (apartment.mark != null)
                              Container(
                                margin: const EdgeInsets.all(8),
                                padding: const EdgeInsets.symmetric(
                                    horizontal: 8, vertical: 4),
                                decoration: BoxDecoration(
                                  color: Colors.black.withOpacity(0.6),
                                  borderRadius: BorderRadius.circular(20),
                                ),
                                child: Row(
                                  mainAxisSize: MainAxisSize.min,
                                  children: [
                                    const Icon(Icons.star,
                                        color: Colors.yellow, size: 16),
                                    const SizedBox(width: 4),
                                    Text(
                                      apartment.mark!.toStringAsFixed(2),
                                      style: const TextStyle(
                                          color: Colors.white,
                                          fontWeight: FontWeight.bold),
                                    ),
                                  ],
                                ),
                              ),
                          ],
                        ),
                      ),
                    Padding(
                      padding: const EdgeInsets.all(12.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Flexible(
                                child: Text(
                                  apartment.address ?? 'No Address',
                                  style: Theme.of(context)
                                      .textTheme
                                      .titleMedium
                                      ?.copyWith(
                                          fontWeight: FontWeight.bold),
                                  overflow: TextOverflow.ellipsis,
                                ),
                              ),
                              Text(
                                '${apartment.price ?? 'N/A'} MAD',
                                style: Theme.of(context)
                                    .textTheme
                                    .titleMedium
                                    ?.copyWith(
                                      color:
                                          Theme.of(context).colorScheme.primary,
                                      fontWeight: FontWeight.bold,
                                    ),
                              ),
                            ],
                          ),
                          const SizedBox(height: 8.0),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.spaceAround,
                            children: [
                              _buildRatingItem(Icons.high_quality, 'Quality',
                                  apartment.quality),
                              _buildRatingItem(Icons.location_on, 'Location',
                                  apartment.location),
                              _buildRatingItem(
                                  Icons.palette, 'Aesthetics', apartment.aesthetics),
                              _buildRatingItem(Icons.chair, 'Furniture',
                                  apartment.furniture),
                            ],
                          ),
                          if (apartment.observation != null &&
                              apartment.observation!.isNotEmpty) ...[
                            const SizedBox(height: 8.0),
                            const Divider(),
                            const SizedBox(height: 4.0),
                            Row(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Icon(Icons.comment,
                                    color: Colors.grey[600], size: 18),
                                const SizedBox(width: 8),
                                Expanded(
                                  child: Text(
                                    '${apartment.observation}',
                                    style: TextStyle(
                                        fontStyle: FontStyle.italic,
                                        color: Colors.grey[700],
                                        fontSize: 13
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ],
                          const SizedBox(height: 4.0),
                          const Divider(),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.end,
                            children: [
                              if (apartment.link != null)
                                TextButton.icon(
                                  icon: const Icon(Icons.link, size: 18,),
                                  onPressed: () => _launchUrl(apartment.link),
                                  label: const Text('Ad'),
                                ),
                              const SizedBox(width: 8),
                              if (apartment.chatLink != null)
                                TextButton.icon(
                                  icon: const Icon(Icons.chat, size: 18),
                                  onPressed: () =>
                                      _launchUrl(apartment.chatLink),
                                  label: const Text('Chat'),
                                ),
                              const SizedBox(width: 8),
                              TextButton.icon(
                                icon: const Icon(Icons.edit, size: 18),
                                onPressed: () {
                                  Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                      builder: (context) =>
                                          EditApartmentScreen(
                                              apartment: apartment),
                                    ),
                                  ).then((_) => _refreshApartments());
                                },
                                label: const Text('Edit'),
                              ),
                            ],
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              );
            },
          );
        },
      ),
    );
  }

  Widget _buildRatingItem(IconData icon, String label, int? value) {
    return Column(
      children: [
        Icon(icon, color: Theme.of(context).colorScheme.secondary, size: 24),
        const SizedBox(height: 2),
        Text(value?.toString() ?? 'N/A',
            style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 13)),
        const SizedBox(height: 2),
        Text(label, style: TextStyle(fontSize: 11, color: Colors.grey[600])),
      ],
    );
  }
}
